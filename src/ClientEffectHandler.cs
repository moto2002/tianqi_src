using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine;
using XEngineActor;
using XEngineCommand;

public class ClientEffectHandler
{
	protected static ClientEffectHandler instance;

	protected List<uint> effectDelayTimerList = new List<uint>();

	protected List<uint> effectCameraTimerList = new List<uint>();

	protected List<uint> effectLoopTimerList = new List<uint>();

	public static ClientEffectHandler Instance
	{
		get
		{
			if (ClientEffectHandler.instance == null)
			{
				ClientEffectHandler.instance = new ClientEffectHandler();
			}
			return ClientEffectHandler.instance;
		}
	}

	public void ResetEffect()
	{
		for (int i = 0; i < this.effectDelayTimerList.get_Count(); i++)
		{
			TimerHeap.DelTimer(this.effectDelayTimerList.get_Item(i));
		}
		this.effectDelayTimerList.Clear();
		for (int j = 0; j < this.effectCameraTimerList.get_Count(); j++)
		{
			TimerHeap.DelTimer(this.effectCameraTimerList.get_Item(j));
		}
		this.effectCameraTimerList.Clear();
		for (int k = 0; k < this.effectLoopTimerList.get_Count(); k++)
		{
			TimerHeap.DelTimer(this.effectLoopTimerList.get_Item(k));
		}
		this.effectLoopTimerList.Clear();
		if (ShakeCamera.instance)
		{
			ShakeCamera.instance.ResetData();
		}
	}

	public void TriggerEffect(int delay, EffectMessage message)
	{
		if (delay == 0)
		{
			this.InitEffect(message);
		}
		else
		{
			this.effectDelayTimerList.Add(TimerHeap.AddTimer<EffectMessage>((uint)delay, 0, new Action<EffectMessage>(this.InitEffect), message));
		}
	}

	protected void InitEffect(EffectMessage message)
	{
		if ((message.caster.IsEntitySelfType || message.caster.OwnerID == EntityWorld.Instance.EntSelf.ID || message.caster.IsEntityMonsterType) && message.effectData.cameraRelyPickup == 0)
		{
			for (int i = 0; i < message.effectData.cameraEffect.get_Count(); i++)
			{
				int cameraEffectID = message.effectData.cameraEffect.get_Item(i);
				this.effectCameraTimerList.Add(TimerHeap.AddTimer((uint)(20 * i), 0, delegate
				{
					this.HandleCameraEffect(cameraEffectID);
				}));
			}
		}
		this.StartEffect(false, message);
		int count = message.effectData.time - 1;
		if (count > 0)
		{
			uint effectLoopTimer = 0u;
			effectLoopTimer = TimerHeap.AddTimer((uint)message.effectData.interval, message.effectData.interval, delegate
			{
				this.StartEffect(true, message);
				count--;
				if (count <= 0)
				{
					TimerHeap.DelTimer(effectLoopTimer);
				}
			});
			this.effectLoopTimerList.Add(effectLoopTimer);
		}
	}

	protected void HandleCameraEffect(int id)
	{
		if (ShakeCamera.instance != null)
		{
			ShakeCamera.instance.HandleCameraEffect(id);
		}
	}

	protected void StartEffect(bool isRepeat, EffectMessage message)
	{
		if (message.effectData.bullet > 0)
		{
			this.CastBullet(message.effectData.bullet, new Action<bool, EffectMessage>(this.HandleEffect), isRepeat, message);
		}
		else
		{
			this.HandleEffect(isRepeat, message);
		}
		if (message.effectData.audio > 0)
		{
			AudioPlayer component = message.casterActor.GetComponent<AudioPlayer>();
			component.RoleId = (long)message.effectData.id;
			SoundManager.Instance.PlayPlayer(component, message.effectData.audio);
		}
	}

	protected void CastBullet(int guid, Action<bool, EffectMessage> callback, bool isRepeat, EffectMessage message)
	{
		if (!message.casterActor)
		{
			return;
		}
		CommandCenter.ExecuteCommand(message.casterActor.get_transform(), new BulletFXCmd
		{
			fxID = guid,
			point = this.GetBulletPoint(message.casterActor, (float)message.effectData.bulletRange),
			offset = message.effectData.bulletOffset,
			bulletLife = message.effectData.collisionTimes,
			collisionCallback = delegate(Actor bullet, XPoint bulletBasePoint, ActorParent targetActor)
			{
				if (targetActor == null)
				{
					return;
				}
				if (targetActor.GetEntity() == null)
				{
					return;
				}
				if (!this.CheckBulletCamp(message.caster, targetActor.GetEntity(), message.effectData.targetType))
				{
					return;
				}
				if (message.effectData.bulletCameraEffect > 0)
				{
					this.HandleCameraEffect(message.effectData.bulletCameraEffect);
				}
				if (message.effectData.bulletFx > 0 && bullet != null)
				{
					FXManager.Instance.PlayFX(message.effectData.bulletFx, null, bullet.get_transform().get_position(), bullet.get_transform().get_rotation(), 1f, 1f, 0, false, 0, null, null, 1f, FXClassification.Normal);
				}
				callback.Invoke(false, new EffectMessage
				{
					caster = message.caster,
					casterActor = bullet,
					skillData = message.skillData,
					effectData = message.effectData,
					basePoint = bulletBasePoint,
					UID = message.UID,
					isClientHandle = true
				});
			}
		});
	}

	protected XPoint GetBulletPoint(Actor actor, float range)
	{
		if (actor == null)
		{
			return null;
		}
		if (range > 0f)
		{
			Vector2 vector = Random.get_insideUnitCircle() * range;
			return new XPoint
			{
				position = new Vector3(actor.get_transform().get_position().x + vector.x, actor.get_transform().get_position().y, actor.get_transform().get_position().z + vector.y),
				rotation = actor.get_transform().get_rotation()
			};
		}
		return new XPoint
		{
			position = actor.get_transform().get_position(),
			rotation = actor.get_transform().get_rotation()
		};
	}

	protected bool CheckBulletCamp(EntityParent shooter, EntityParent beShot, int targetType)
	{
		switch (targetType)
		{
		case 1:
			return shooter.Camp != beShot.Camp;
		case 2:
			return shooter.Camp == beShot.Camp && shooter.ID != beShot.ID;
		case 3:
			return shooter.ID == beShot.ID;
		case 4:
			return shooter.Camp == beShot.Camp;
		case 5:
			return shooter.OwnerID == beShot.ID;
		case 6:
			return true;
		case 7:
			return shooter.OwnedIDs.Contains(beShot.ID);
		case 8:
			return shooter.DamageSourceID == beShot.ID;
		case 9:
			return EntityWorld.Instance.EntSelf.ID == beShot.ID;
		case 10:
			return beShot.IsLogicBoss;
		case 11:
			return beShot.IsBuffEntity;
		default:
			return false;
		}
	}

	protected void HandleEffect(bool isRepeat, EffectMessage message)
	{
		EntityParent caster = message.caster;
		if (caster == null)
		{
			return;
		}
		if (caster.IsDead || !caster.IsFighting)
		{
			return;
		}
		Actor casterActor = message.casterActor;
		if (casterActor == null)
		{
			return;
		}
		Effect effectData = message.effectData;
		XPoint basePoint = message.basePoint;
		List<EffectTargetInfo> list = new List<EffectTargetInfo>();
		switch (effectData.type1)
		{
		case 3:
		case 8:
		case 9:
		case 10:
			goto IL_484;
		}
		if (basePoint != null)
		{
			List<EntityParent> list2 = this.CheckCandidateByType(caster, casterActor, effectData.targetType, effectData.forcePickup == 1, effectData.antiaircraft);
			if (list2.get_Count() != 0)
			{
				Hashtable hashtable = this.CheckCandidatesByEffectShape(list2, ContainerGear.containers.Values, casterActor, basePoint, effectData);
				if (hashtable.get_Count() != 0)
				{
					List<EntityParent> list3 = hashtable.get_Item("Entity") as List<EntityParent>;
					List<ContainerGear> list4 = hashtable.get_Item("Container") as List<ContainerGear>;
					if (list3.get_Count() > 0)
					{
						List<float> list5 = new List<float>();
						for (int i = 0; i < list3.get_Count(); i++)
						{
							if (!SystemConfig.IsEffectOn)
							{
								break;
							}
							if (list3.get_Item(i) != null)
							{
								if (list3.get_Item(i).Actor)
								{
									list.Add(new EffectTargetInfo
									{
										targetId = list3.get_Item(i).ID
									});
									if (caster.IsEntitySelfType && (effectData.type1 == 1 || effectData.type1 == 5 || effectData.type1 == 11))
									{
										BattleBlackboard.Instance.ContinueCombo = true;
									}
									AvatarModel avatarModel = DataReader<AvatarModel>.Get(list3.get_Item(i).FixModelID);
									if ((!isRepeat || effectData.cycleHit != 0) && (!list3.get_Item(i).IsUnconspicuous || effectData.forcePickup != 0) && effectData.hitFx != 0)
									{
										if (casterActor is ActorParent)
										{
											list3.get_Item(i).Actor.PlayHitFx((casterActor as ActorParent).FixTransform, effectData.hitFx, avatarModel.undAtkFxScale, avatarModel.undAtkFxOffset);
										}
										else
										{
											list3.get_Item(i).Actor.PlayHitFx(casterActor.get_transform(), effectData.hitFx, avatarModel.undAtkFxScale, avatarModel.undAtkFxOffset);
										}
									}
									if (InstanceManager.IsServerBattle)
									{
										list3.get_Item(i).Actor.PlayHitSound(effectData.hitAudio);
									}
									list5.Add(avatarModel.frameRatio);
								}
							}
						}
						if (!isRepeat)
						{
							if ((message.caster.IsEntitySelfType || message.caster.OwnerID == EntityWorld.Instance.EntSelf.ID || message.caster.IsEntityMonsterType) && effectData.cameraRelyPickup == 1)
							{
								for (int j = 0; j < message.effectData.cameraEffect.get_Count(); j++)
								{
									int cameraEffectID = message.effectData.cameraEffect.get_Item(j);
									this.effectCameraTimerList.Add(TimerHeap.AddTimer((uint)(20 * j), 0, delegate
									{
										this.HandleCameraEffect(cameraEffectID);
									}));
								}
							}
							list5.Sort();
							CommandCenter.ExecuteCommand(casterActor.get_transform(), new FrozeFrameCmd
							{
								count = list3.get_Count(),
								rate = effectData.frameFroze,
								time = effectData.frameTime,
								timeRateList = list5,
								interval = effectData.frameInterval,
								callback = null
							});
						}
					}
					if (list4.get_Count() > 0 && effectData.type1 == 1 && effectData.targetType == 1)
					{
						for (int k = 0; k < list4.get_Count(); k++)
						{
							list4.get_Item(k).OnHit(effectData.id);
						}
					}
					if (casterActor is ActorFX && (list3.get_Count() > 0 || list4.get_Count() > 0))
					{
						(casterActor as ActorFX).bulletLife--;
					}
				}
			}
		}
		IL_484:
		if (isRepeat)
		{
			GlobalBattleNetwork.Instance.SendUpdateEffect(message.caster.ID, (message.skillData != null) ? message.skillData.id : 0, message.effectData.id, list, message.UID, message.basePoint, message.isClientHandle);
		}
		else
		{
			GlobalBattleNetwork.Instance.SendAddEffect(message.caster.ID, (message.skillData != null) ? message.skillData.id : 0, message.effectData.id, list, message.UID, message.basePoint, message.isClientHandle);
		}
	}

	protected List<EntityParent> CheckCandidateByType(EntityParent owner, Actor ownerActor, int targetType, bool isIgnoreUnconspicuous, int altitude)
	{
		List<EntityParent> list = new List<EntityParent>();
		if (owner == null)
		{
			return list;
		}
		if (ownerActor == null)
		{
			return list;
		}
		switch (targetType)
		{
		case 1:
			EntityWorld.Instance.GetAllEffectTarget(owner, isIgnoreUnconspicuous, altitude, owner.Camp, false, false, list);
			break;
		case 2:
			EntityWorld.Instance.GetAllEffectTarget(owner, isIgnoreUnconspicuous, altitude, owner.Camp, true, false, list);
			break;
		case 3:
			list.Add(owner);
			break;
		case 4:
			EntityWorld.Instance.GetAllEffectTarget(owner, isIgnoreUnconspicuous, altitude, owner.Camp, true, true, list);
			break;
		case 5:
			if (EntityWorld.Instance.StateFilter<EntityParent>(owner.Owner, isIgnoreUnconspicuous) && EntityWorld.Instance.AltitudeFilter<EntityParent>(owner.Owner, altitude))
			{
				list.Add(owner.Owner);
			}
			break;
		case 6:
			EntityWorld.Instance.GetAllEffectTarget(owner, isIgnoreUnconspicuous, altitude, -1, false, true, list);
			break;
		case 7:
		{
			List<EntityParent> values = EntityWorld.Instance.GetEntities<EntityPet>().Values;
			for (int i = 0; i < values.get_Count(); i++)
			{
				if (values.get_Item(i).OwnerID == owner.ID && EntityWorld.Instance.StateFilter<EntityParent>(values.get_Item(i), isIgnoreUnconspicuous) && EntityWorld.Instance.AltitudeFilter<EntityParent>(values.get_Item(i), altitude))
				{
					list.Add(values.get_Item(i));
				}
			}
			break;
		}
		case 8:
			if (owner.DamageSource != null && owner.DamageSource.Actor && EntityWorld.Instance.StateFilter<EntityParent>(owner.DamageSource, isIgnoreUnconspicuous) && EntityWorld.Instance.AltitudeFilter<EntityParent>(owner.DamageSource, altitude))
			{
				list.Add(owner.DamageSource);
			}
			break;
		case 9:
			if (EntityWorld.Instance.StateFilter<EntitySelf>(EntityWorld.Instance.EntSelf, isIgnoreUnconspicuous) && EntityWorld.Instance.AltitudeFilter<EntitySelf>(EntityWorld.Instance.EntSelf, altitude))
			{
				list.Add(EntityWorld.Instance.EntSelf);
			}
			break;
		case 10:
		{
			List<EntityParent> values2 = EntityWorld.Instance.GetEntities<EntityMonster>().Values;
			for (int j = 0; j < values2.get_Count(); j++)
			{
				if (values2.get_Item(j).IsLogicBoss && EntityWorld.Instance.StateFilter<EntityParent>(values2.get_Item(j), isIgnoreUnconspicuous) && EntityWorld.Instance.AltitudeFilter<EntityParent>(values2.get_Item(j), altitude))
				{
					list.Add(values2.get_Item(j));
				}
			}
			break;
		}
		case 11:
		{
			List<EntityParent> values3 = EntityWorld.Instance.GetEntities<EntityMonster>().Values;
			for (int k = 0; k < values3.get_Count(); k++)
			{
				if (values3.get_Item(k).IsBuffEntity && EntityWorld.Instance.StateFilter<EntityParent>(values3.get_Item(k), isIgnoreUnconspicuous) && EntityWorld.Instance.AltitudeFilter<EntityParent>(values3.get_Item(k), altitude))
				{
					list.Add(values3.get_Item(k));
				}
			}
			break;
		}
		}
		return list;
	}

	protected Hashtable CheckCandidatesByEffectShape(List<EntityParent> candidates, List<ContainerGear> containers, Actor casterActor, XPoint basePoint, Effect effectData)
	{
		Hashtable hashtable = new Hashtable();
		if (casterActor == null)
		{
			return hashtable;
		}
		if (basePoint == null)
		{
			return hashtable;
		}
		List<EntityParent> list = new List<EntityParent>();
		List<ContainerGear> list2 = new List<ContainerGear>();
		GraghMessage graghMessage = null;
		GraghMessage graghMessage2 = null;
		bool flag = true;
		XPoint xPoint = basePoint.ApplyOffset(effectData.offset).ApplyOffset(effectData.offset2).ApplyForwardFix(effectData.forwardFixAngle);
		if (effectData.range.get_Count() > 2)
		{
			int num = Mathf.Abs(effectData.range.get_Item(0));
			if (num != 1)
			{
				if (num == 2)
				{
					graghMessage = new GraghMessage(new XPoint
					{
						position = xPoint.position,
						rotation = xPoint.rotation
					}, GraghShape.Rect, 0f, 0f, (float)effectData.range.get_Item(1) * 0.01f, (float)effectData.range.get_Item(2) * 0.01f);
				}
			}
			else
			{
				graghMessage = new GraghMessage(new XPoint
				{
					position = xPoint.position,
					rotation = xPoint.rotation
				}, GraghShape.Sector, (float)effectData.range.get_Item(1) * 0.01f, (float)effectData.range.get_Item(2), 0f, 0f);
			}
		}
		if (effectData.range2.get_Count() > 2)
		{
			if (effectData.range2.get_Item(0) < 0)
			{
				flag = false;
			}
			int num = Mathf.Abs(effectData.range2.get_Item(0));
			if (num != 1)
			{
				if (num == 2)
				{
					graghMessage2 = new GraghMessage(new XPoint
					{
						position = xPoint.position,
						rotation = xPoint.rotation
					}, GraghShape.Rect, 0f, 0f, (float)effectData.range2.get_Item(1) * 0.01f, (float)effectData.range2.get_Item(2) * 0.01f);
				}
			}
			else
			{
				graghMessage2 = new GraghMessage(new XPoint
				{
					position = xPoint.position,
					rotation = xPoint.rotation
				}, GraghShape.Sector, (float)effectData.range2.get_Item(1) * 0.01f, (float)effectData.range2.get_Item(2), 0f, 0f);
			}
		}
		if (graghMessage != null)
		{
			graghMessage.DrawShape(Color.get_red());
		}
		if (graghMessage2 != null)
		{
			if (flag)
			{
				graghMessage2.DrawShape(Color.get_red());
			}
			else
			{
				graghMessage2.DrawShape(Color.get_blue());
			}
		}
		for (int i = 0; i < candidates.get_Count(); i++)
		{
			if (candidates.get_Item(i) != null)
			{
				if (candidates.get_Item(i).Actor)
				{
					bool flag2 = false;
					float hitRadius = XUtility.GetHitRadius(candidates.get_Item(i).Actor.FixTransform);
					bool flag3 = graghMessage != null && graghMessage.InArea(candidates.get_Item(i).Actor.FixTransform.get_position(), hitRadius);
					bool flag4 = graghMessage2 != null && graghMessage2.InArea(candidates.get_Item(i).Actor.FixTransform.get_position(), hitRadius);
					if (flag)
					{
						if (flag3 || flag4)
						{
							flag2 = true;
						}
					}
					else if (flag3 && !flag4)
					{
						flag2 = true;
					}
					if (flag2)
					{
						list.Add(candidates.get_Item(i));
					}
				}
			}
		}
		for (int j = 0; j < containers.get_Count(); j++)
		{
			if (containers.get_Item(j))
			{
				if (containers.get_Item(j).get_gameObject())
				{
					bool flag5 = false;
					bool flag6 = graghMessage != null && graghMessage.InArea(containers.get_Item(j).get_transform().get_position(), containers.get_Item(j).hitRange);
					bool flag7 = graghMessage2 != null && graghMessage2.InArea(containers.get_Item(j).get_transform().get_position(), containers.get_Item(j).hitRange);
					if (flag)
					{
						if (flag6 || flag7)
						{
							flag5 = true;
						}
					}
					else if (flag6 && !flag7)
					{
						flag5 = true;
					}
					if (flag5)
					{
						list2.Add(containers.get_Item(j));
					}
				}
			}
		}
		hashtable.Add("Entity", list);
		hashtable.Add("Container", list2);
		return hashtable;
	}
}
