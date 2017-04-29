using Foundation.EF;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;
using XEngineCommand;

namespace XEngineActor
{
	public class ActorModel : Actor, IAnimator
	{
		public const int NavMeshFixDistance = 50;

		private Animator mAnimator;

		protected ActorModelType modelType;

		private string modelLayer = "CameraRange";

		private Vector3 _InitLocalPosition = Vector3.get_zero();

		protected bool isFirstSetActionEnd;

		protected bool isFirstPlayActionEnd = true;

		private bool isShowing = true;

		private List<Renderer> renderers = new List<Renderer>();

		private Action m_uiDisplaySkillEnd;

		public int nameId;

		private int mCurrentSkill;

		protected EquipCustomization mEquipCustomizationer;

		protected Dictionary<int, EffectMaterial> effectMessageCache = new Dictionary<int, EffectMaterial>();

		protected List<uint> effectDelayTimerList = new List<uint>();

		protected List<uint> effectLoopTimerList = new List<uint>();

		protected Vector3 hitMoveDirection = Vector3.get_zero();

		protected float hitMoveSpeed;

		protected NavMeshHit fixHit = default(NavMeshHit);

		private string curActionName = string.Empty;

		protected Dictionary<string, Action> ActionEndCallback = new Dictionary<string, Action>();

		protected NavMeshAgent navAgent;

		protected Action navEndCallBack;

		public ActorModelType ModelType
		{
			get
			{
				return this.modelType;
			}
			set
			{
				this.modelType = value;
			}
		}

		public string ModelLayer
		{
			get
			{
				return this.modelLayer;
			}
			set
			{
				this.modelLayer = value;
				this.SetGameObjectLayer();
			}
		}

		public Vector3 InitLocalPosition
		{
			get
			{
				return this._InitLocalPosition;
			}
			set
			{
				this._InitLocalPosition = value;
				this.ResetPosition();
			}
		}

		protected bool IsFirstSetActionEnd
		{
			get
			{
				return this.isFirstSetActionEnd;
			}
			set
			{
				this.isFirstSetActionEnd = value;
			}
		}

		protected bool IsFirstPlayActionEnd
		{
			get
			{
				return this.isFirstPlayActionEnd;
			}
			set
			{
				this.isFirstPlayActionEnd = value;
			}
		}

		protected bool IsShowing
		{
			get
			{
				return this.isShowing;
			}
			set
			{
				this.isShowing = value;
			}
		}

		public EquipCustomization EquipCustomizationer
		{
			get
			{
				if (this.mEquipCustomizationer == null)
				{
					this.mEquipCustomizationer = new EquipCustomization();
				}
				return this.mEquipCustomizationer;
			}
		}

		public string CurActionName
		{
			get
			{
				return this.curActionName;
			}
			set
			{
				this.curActionName = value;
			}
		}

		public NavMeshAgent NavAgent
		{
			get
			{
				return (!this.navAgent) ? (this.navAgent = base.get_gameObject().AddMissingComponent<NavMeshAgent>()) : this.navAgent;
			}
		}

		public Action NavEndCallBack
		{
			get
			{
				return this.navEndCallBack;
			}
			set
			{
				this.navEndCallBack = value;
			}
		}

		private Animator GetAnimator()
		{
			if (this.mAnimator == null)
			{
				this.mAnimator = base.GetComponentInChildren<Animator>();
				this.mAnimator.set_applyRootMotion(false);
				bool inBattle = false;
				if (EntityWorld.Instance != null && EntityWorld.Instance.EntSelf != null)
				{
					inBattle = EntityWorld.Instance.EntSelf.IsInBattle;
				}
				AssetManager.AssetOfControllerManager.SetController(this.mAnimator, this.resGUID, inBattle);
			}
			return this.mAnimator;
		}

		private void DoUIDisplaySkillEnd()
		{
			if (this.m_uiDisplaySkillEnd != null)
			{
				this.m_uiDisplaySkillEnd.Invoke();
				this.m_uiDisplaySkillEnd = null;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			this.InitRenderers();
			this.IsShowing = true;
			this.ShowSelf(false);
			this.ResetAll();
		}

		protected override void OnCreate()
		{
			base.get_gameObject().AddUniqueComponent<AudioPlayer>();
			base.addColleague(new ActorParent.AnimatorSpeedSystem());
			base.addColleague(new ActorParent.ActionSystem());
			base.addColleague(new ActorParent.FXSystem());
			base.OnCreate();
		}

		protected override void OnDestroy()
		{
			base.deleteColleague(typeof(ActorParent.AnimatorSpeedSystem));
			base.deleteColleague(typeof(ActorParent.ActionSystem));
			base.deleteColleague(typeof(ActorParent.FXSystem));
			this.RemoveFxs();
			this.RemoveWeaponFX();
			if (this.mEquipCustomizationer != null)
			{
				this.mEquipCustomizationer.Release();
			}
			base.OnDestroy();
		}

		private void OnDisable()
		{
			this.RemoveFxs();
			this.RemoveWeaponFX();
		}

		private void Update()
		{
			if (this.ModelType == ActorModelType.CG && this.NavAgent.get_enabled() && this.NavAgent.get_pathStatus() == null && this.NavAgent.get_remainingDistance() <= 0f && this.NavEndCallBack != null)
			{
				this.NavEndCallBack.Invoke();
				this.NavAgent.set_enabled(false);
				this.NavEndCallBack = null;
			}
			if (!this.IsFirstPlayActionEnd)
			{
				this.CheckCanShowModel();
			}
		}

		private void CheckCanShowModel()
		{
			if (this.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName(this.CurActionName))
			{
				this.ShowSelf(true);
			}
			else
			{
				this.ShowSelf(false);
			}
		}

		private void InitRenderers()
		{
			this.renderers.Clear();
			Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
			if (componentsInChildren != null && componentsInChildren.Length > 0)
			{
				this.renderers.AddRange(componentsInChildren);
			}
		}

		public void ShowSelf(bool isShow)
		{
			if (this.IsShowing == isShow)
			{
				return;
			}
			this.IsShowing = isShow;
			for (int i = 0; i < this.renderers.get_Count(); i++)
			{
				this.renderers.get_Item(i).set_enabled(isShow);
			}
		}

		public override void OnCheckCombo(CheckComboCmd cmd)
		{
			int num = (!ModelDisplayManager.IsAlwaysCombo || !DataReader<Skill>.Contains(this.GetCurrentSkill())) ? 0 : DataReader<Skill>.Get(this.GetCurrentSkill()).combo;
			if (num != 0)
			{
				ModelDisplayManager.ShowSkill(this, num, delegate
				{
					CurrenciesUIViewModel.Show(true);
					PetBasicUIView.Instance.SetRawImageModelLayer(false, true, false);
				});
			}
		}

		public override void OnFXEvent(FXEventCmd cmd)
		{
			AnimationEvent args = cmd.args;
			int intParameter = args.get_intParameter();
			if (intParameter <= 0)
			{
				string text = "播放特效失败, 特效ID不合法";
				UIManagerControl.Instance.ShowToastText(text);
				Debuger.Error(text, new object[0]);
			}
			CommandCenter.ExecuteCommand(base.get_transform(), new PlayActionFXCmd
			{
				fxID = intParameter,
				scale = DataReader<AvatarModel>.Get(this.resGUID).scale
			});
		}

		public override void OnPlayActionFX(PlayActionFXCmd cmd)
		{
			using (List<AbstractColleague>.Enumerator enumerator = this.colleagues.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AbstractColleague current = enumerator.get_Current();
					if (current is ActorParent.FXSystem)
					{
						(current as ActorParent.FXSystem).OnPlayActionFX(cmd);
						break;
					}
				}
			}
		}

		public override void OnAnimationEnd(AnimationEndCmd cmd)
		{
			ActorModelType actorModelType = this.modelType;
			if (actorModelType != ActorModelType.UI)
			{
				if (actorModelType == ActorModelType.CG)
				{
					this.OnCGAnimationEnd(cmd);
				}
			}
			else
			{
				this.OnUIAnimationEnd(cmd);
			}
		}

		protected void OnUIAnimationEnd(AnimationEndCmd cmd)
		{
			if (XUtility.StartsWith(cmd.actName, "idle") || XUtility.StartsWith(cmd.actName, "die"))
			{
				this.DoUIDisplaySkillEnd();
				return;
			}
			this.ResetAll();
			this.RemoveFxs();
			if (ControllerTool.SplitIsCity(this.GetAnimator()))
			{
				this.PreciseSetAction("idle_city");
			}
			else
			{
				this.PreciseSetAction("idle");
			}
			this.DoUIDisplaySkillEnd();
		}

		protected void OnCGAnimationEnd(AnimationEndCmd cmd)
		{
			if (this.ActionEndCallback.ContainsKey(this.CurActionName))
			{
				Action action = this.ActionEndCallback.get_Item(this.CurActionName);
				if (action != null)
				{
					this.ActionEndCallback.Remove(this.CurActionName);
					action.Invoke();
				}
			}
			if (DataReader<Action>.Get(cmd.actName).loop == 0 && cmd.actName != "die" && this.CurActionName != "victory")
			{
				this.SetAction("idle", null);
			}
		}

		public override void OnSkillEffect(SkillEffectCmd cmd)
		{
			if (this.filterEvent(cmd.args.get_stringParameter()))
			{
				this.ClientTriggerEffect(cmd.args.get_intParameter());
			}
		}

		public override void OnAudioEvent(AudioEventCmd cmd)
		{
			if (this.filterEvent(cmd.args.get_stringParameter()))
			{
				SoundManager.Instance.PlayPlayer(base.get_transform().GetComponent<AudioPlayer>(), cmd.args.get_intParameter());
			}
		}

		public override void OnAudio2DEvent(Audio2DEventCmd cmd)
		{
			if (this.filterEvent(cmd.args.get_stringParameter()))
			{
				SoundManager.PlayUI(cmd.args.get_intParameter(), false);
			}
		}

		private void RemoveFxs()
		{
			CommandCenter.ExecuteCommand(base.get_transform(), new RemoveAllFXCmd());
		}

		public bool filterEvent(string tag)
		{
			return string.IsNullOrEmpty(tag) || this.GetCurrentSkill() == 0 || DataReader<Skill>.Get(this.GetCurrentSkill()).eventTag.Equals(tag);
		}

		public int GetCurrentActionHash()
		{
			return this.GetAnimator().GetCurrentAnimatorStateInfo(0).get_fullPathHash();
		}

		private void RemoveActionFX()
		{
			CommandCenter.ExecuteCommand(base.get_transform(), new RemoveActionFXCmd());
		}

		public override void OnRemoveActionFX(RemoveActionFXCmd cmd)
		{
			using (List<AbstractColleague>.Enumerator enumerator = this.colleagues.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AbstractColleague current = enumerator.get_Current();
					if (current is ActorParent.FXSystem)
					{
						(current as ActorParent.FXSystem).OnRemoveActionFX(cmd);
						break;
					}
				}
			}
		}

		private int GetCurrentSkill()
		{
			return this.mCurrentSkill;
		}

		private void RemoveWeaponFX()
		{
			this.EquipCustomizationer.RemoveWeaponFX();
		}

		public void EquipOn(int id, int gogokNum = 0)
		{
			this.EquipCustomizationer.ActorTarget = this;
			this.EquipCustomizationer.EquipGogokNum = gogokNum;
			this.EquipCustomizationer.EquipOn(id);
		}

		public void EquipWingOn(int wingModelId)
		{
			this.EquipCustomizationer.ActorTarget = this;
			this.EquipCustomizationer.EquipWingOn(wingModelId);
		}

		public void EquipSuccess()
		{
			this.SetGameObjectLayer();
		}

		public void EquipWingSuccess(Animator wing_tor)
		{
			this.SetGameObjectLayer();
		}

		protected void CastSkill(Skill skillData)
		{
			for (int i = 0; i < skillData.effect.get_Count(); i++)
			{
				this.MarkStaticEffectMessage(skillData.effect.get_Item(i));
			}
			if (!string.IsNullOrEmpty(skillData.attAction))
			{
				List<int> actionEffects = XUtility.GetActionEffects(this.GetAnimator().get_runtimeAnimatorController(), skillData.attAction);
				for (int j = 0; j < actionEffects.get_Count(); j++)
				{
					this.MarkStaticEffectMessage(actionEffects.get_Item(j));
				}
			}
			for (int k = 0; k < skillData.effect.get_Count(); k++)
			{
				this.ClientTriggerEffect(skillData.effect.get_Item(k));
			}
		}

		protected void MarkStaticEffectMessage(int effectID)
		{
			Effect effect = DataReader<Effect>.Get(effectID);
			if (effect == null)
			{
				return;
			}
			EffectMaterial effectMaterial = new EffectMaterial();
			effectMaterial.basePoint = ((effect.type2 != 3 && effect.type2 != 4) ? this.GetEffectBasePoint((EffectBasePointType)effect.@base, (float)effect.tremble, 0L, effect.summonId, effect.coord, effect.orientation) : null);
			if (!this.effectMessageCache.ContainsKey(effectID))
			{
				this.effectMessageCache.Add(effectID, effectMaterial);
			}
			else
			{
				this.effectMessageCache.set_Item(effectID, effectMaterial);
			}
		}

		protected void ClientTriggerEffect(int id)
		{
			Effect effect = DataReader<Effect>.Get(id);
			if (effect == null)
			{
				return;
			}
			EffectMaterial effectMaterial = (!this.effectMessageCache.ContainsKey(id)) ? null : this.effectMessageCache.get_Item(id);
			EffectMessage effectMessage = new EffectMessage();
			effectMessage.casterActor = this;
			effectMessage.effectData = effect;
			if (effect.type2 == 4 || effect.type2 == 3)
			{
				effectMessage.basePoint = this.GetEffectBasePoint((EffectBasePointType)effect.@base, (float)effect.tremble, (effectMaterial != null) ? effectMaterial.skillTargetID : 0L, effect.summonId, effect.coord, effect.orientation);
			}
			else
			{
				effectMessage.basePoint = ((effectMaterial != null) ? effectMaterial.basePoint : null);
			}
			this.TriggerEffect(effect.delay, effectMessage);
			if (effectMessage.basePoint != null && effect.fx != 0)
			{
				CommandCenter.ExecuteCommand(base.get_transform(), new BulletFXCmd
				{
					fxID = effect.fx,
					point = effectMessage.basePoint,
					useY = true
				});
			}
		}

		protected XPoint GetEffectBasePoint(EffectBasePointType basePointType, float shakeRange, long targetID, int spawnPointID, List<int> prescribedPositionList, List<int> prescribedToPositionList)
		{
			Vector3 zero = Vector3.get_zero();
			if (shakeRange != 0f)
			{
				Vector2 vector = Random.get_insideUnitCircle() * shakeRange / 100f;
				zero = new Vector3(vector.x, 0f, vector.y);
			}
			switch (basePointType)
			{
			case EffectBasePointType.Self:
				return new XPoint
				{
					position = base.get_transform().get_position() + zero,
					rotation = base.get_transform().get_rotation()
				};
			case EffectBasePointType.Target:
				return (!EntityWorld.Instance.AllEntities.ContainsKey(targetID) || !EntityWorld.Instance.AllEntities[targetID].Actor) ? null : new XPoint
				{
					position = EntityWorld.Instance.AllEntities[targetID].Actor.FixTransform.get_position() + zero,
					rotation = EntityWorld.Instance.AllEntities[targetID].Actor.FixTransform.get_rotation()
				};
			case EffectBasePointType.SpawnPoint:
			{
				if (spawnPointID == 0)
				{
					return null;
				}
				Vector2 point = MapDataManager.Instance.GetPoint(MySceneManager.Instance.CurSceneID, spawnPointID);
				Vector3 terrainPoint = MySceneManager.GetTerrainPoint(point.x, point.y, base.get_transform().get_position().y);
				Vector3 vector2 = Vector3.get_forward();
				if (prescribedToPositionList != null && prescribedToPositionList.get_Count() >= 3)
				{
					vector2 = new Vector3((float)prescribedToPositionList.get_Item(0) * 0.01f, (float)prescribedToPositionList.get_Item(1) * 0.01f, (float)prescribedToPositionList.get_Item(2) * 0.01f) - terrainPoint;
				}
				return new XPoint
				{
					position = terrainPoint + zero,
					rotation = Quaternion.LookRotation(vector2)
				};
			}
			case EffectBasePointType.TriggerPoint:
				return null;
			case EffectBasePointType.PrescribedPoint:
			{
				if (prescribedPositionList == null)
				{
					return null;
				}
				if (prescribedPositionList.get_Count() < 3)
				{
					return null;
				}
				Vector3 vector3 = new Vector3((float)prescribedPositionList.get_Item(0) * 0.01f, (float)prescribedPositionList.get_Item(1) * 0.01f, (float)prescribedPositionList.get_Item(2) * 0.01f);
				Vector3 vector4 = Vector3.get_forward();
				if (prescribedToPositionList != null && prescribedToPositionList.get_Count() >= 3)
				{
					vector4 = new Vector3((float)prescribedToPositionList.get_Item(0) * 0.01f, (float)prescribedToPositionList.get_Item(1) * 0.01f, (float)prescribedToPositionList.get_Item(2) * 0.01f) - vector3;
				}
				return new XPoint
				{
					position = vector3 + zero,
					rotation = Quaternion.LookRotation(vector4)
				};
			}
			default:
				return null;
			}
		}

		protected void TriggerEffect(int delay, EffectMessage message)
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

		public void CastBullet(int guid, Action<bool, EffectMessage> callback, bool isRepeat, EffectMessage message)
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
				collisionCallback = null
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

		protected void HandleEffect(bool isRepeat, EffectMessage message)
		{
			Actor casterActor = message.casterActor;
			if (casterActor == null)
			{
				return;
			}
			Effect effectData = message.effectData;
			XPoint basePoint = message.basePoint;
			EffectType type = (EffectType)effectData.type1;
			if (type == EffectType.Damage)
			{
				if (basePoint != null)
				{
					List<ActorModel> list = this.CheckCandidatesByEffectShape(new List<ActorModel>(), casterActor, basePoint, effectData);
					if (list.get_Count() != 0)
					{
						for (int i = 0; i < list.get_Count(); i++)
						{
							list.get_Item(i).PlayHitFX(casterActor.get_transform(), effectData.hitFx, 1f, new List<int>());
							list.get_Item(i).PlayHitSound(effectData.hitAudio);
							list.get_Item(i).HandleHit(effectData, basePoint);
						}
					}
				}
			}
		}

		protected List<ActorModel> CheckCandidatesByEffectShape(List<ActorModel> candidates, Actor casterActor, XPoint basePoint, Effect effectData)
		{
			List<ActorModel> list = new List<ActorModel>();
			if (casterActor == null)
			{
				return list;
			}
			if (basePoint == null)
			{
				return list;
			}
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
				if (!(candidates.get_Item(i) == null))
				{
					bool flag2 = false;
					float hitRadius = XUtility.GetHitRadius(candidates.get_Item(i).get_transform());
					bool flag3 = graghMessage != null && graghMessage.InArea(candidates.get_Item(i).get_transform().get_position(), hitRadius);
					bool flag4 = graghMessage2 != null && graghMessage2.InArea(candidates.get_Item(i).get_transform().get_position(), hitRadius);
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
			return list;
		}

		public void PlayHitFX(Transform caster, int fxID, float scale, List<int> offsets)
		{
			CommandCenter.ExecuteCommand(base.get_transform(), new HitFXCmd
			{
				fxID = fxID,
				caster = caster,
				scale = scale,
				offsets = offsets
			});
		}

		public void PlayHitSound(int soundID)
		{
			SoundManager.Instance.PlayPlayer(base.get_gameObject().AddUniqueComponent<AudioPlayer>(), soundID);
		}

		public void HandleHit(Effect effectData, XPoint basePoint)
		{
			XPoint xPoint = basePoint.ApplyOffset(effectData.offset);
			string hitAction = this.GetHitAction(effectData.hitAction);
			float num = 0f;
			float hitMoveTime = 0f;
			if (effectData.hitMove != null && effectData.hitMove.get_Count() > 1)
			{
				num = effectData.hitMove.get_Item(0);
				hitMoveTime = effectData.hitMove.get_Item(1);
			}
			Vector3 arg_128_0;
			if (num == 0f)
			{
				arg_128_0 = Vector3.get_zero();
			}
			else if (effectData.hitBase == 1)
			{
				Vector3 vector = new Vector3(xPoint.position.x - base.get_transform().get_position().x, 0f, xPoint.position.z - base.get_transform().get_position().z);
				arg_128_0 = vector.get_normalized();
			}
			else
			{
				Vector3 vector2 = new Vector3(basePoint.position.x - base.get_transform().get_position().x, 0f, basePoint.position.z - base.get_transform().get_position().z);
				arg_128_0 = vector2.get_normalized();
			}
			Vector3 hitMoveDir = arg_128_0;
			if (num == 0f)
			{
				this.ClientPlayHit(hitAction, effectData.hitstraight, effectData.hitActionPriority);
			}
			else
			{
				this.ClientPlayHitMove(hitAction, hitMoveDir, num, hitMoveTime, effectData.hitstraight, effectData.hitActionPriority);
			}
		}

		protected string GetHitAction(string hitAction)
		{
			if (hitAction == "float2" && !this.GetAnimator().HasAction("float2"))
			{
				hitAction = "float";
			}
			if (hitAction == "float" && !this.GetAnimator().HasAction("float"))
			{
				hitAction = "hit";
			}
			return hitAction;
		}

		protected bool ClientPlayHit(string hitAction, int hitStraightTime, int hitPriority)
		{
			if (!this.GetAnimator().HasAction(hitAction))
			{
				return false;
			}
			this.PreciseSetAction(hitAction);
			return true;
		}

		public void ClientPlayHitMove(string hitAction, Vector3 hitMoveDir, float hitMoveDistance, float hitMoveTime, int hitStraightTime, int hitPriority)
		{
			if (this.ClientPlayHit(hitAction, hitStraightTime, hitPriority))
			{
				if (hitMoveTime != 0f && hitMoveDistance != 0f)
				{
					this.MoveTo(base.get_transform().get_position() + hitMoveDir * hitMoveDistance, hitMoveDistance / hitMoveTime, null);
				}
				if (hitMoveDir != Vector3.get_zero())
				{
					base.get_transform().set_forward(hitMoveDir);
				}
			}
		}

		public void FixCurrentPosition()
		{
			if (NavMesh.SamplePosition(base.get_transform().get_position(), ref this.fixHit, 50f, -1))
			{
				base.get_transform().set_position(MySceneManager.GetTerrainPoint(this.fixHit.get_position().x, this.fixHit.get_position().z, base.get_transform().get_position().y));
			}
		}

		public bool SetPosition(Vector2 position, int layer)
		{
			return this.SetPosition(new Vector3(position.x, (float)(30 * layer), position.y));
		}

		public bool SetPosition(Vector3 position)
		{
			Vector3 position2;
			bool result = this.FixNavMeshPosition(position, out position2);
			base.get_transform().set_position(position2);
			return result;
		}

		protected bool FixNavMeshPosition(Vector3 pos, out Vector3 fixPos)
		{
			NavMeshHit navMeshHit;
			if (NavMesh.SamplePosition(pos, ref navMeshHit, 10f, -1))
			{
				fixPos = navMeshHit.get_position();
				return true;
			}
			fixPos = pos;
			return false;
		}

		public void SetForward(Vector3 forward)
		{
			base.get_transform().set_forward(new Vector3(forward.x, 0f, forward.z));
		}

		public void SetAction(string actionName, Action callback = null)
		{
			if (this.ActionEndCallback.ContainsKey(this.CurActionName))
			{
				Action action = this.ActionEndCallback.get_Item(this.CurActionName);
				this.ActionEndCallback.Remove(this.CurActionName);
				if (action != null)
				{
					action.Invoke();
				}
			}
			if (callback != null)
			{
				this.ActionEndCallback.Add(actionName, callback);
			}
			this.PreciseSetAction(actionName);
		}

		public void PreciseSetAction(string actionName)
		{
			this.CurActionName = actionName;
			if (!this.IsFirstPlayActionEnd)
			{
				this.IsFirstPlayActionEnd = true;
				this.ShowSelf(true);
			}
			if (!this.IsFirstSetActionEnd)
			{
				this.IsFirstSetActionEnd = true;
				this.IsFirstPlayActionEnd = false;
				this.ShowSelf(false);
			}
			this.GetAnimator().Play(actionName);
		}

		public void SetBornAction(string actionName, Action callback = null)
		{
			if (this.ActionEndCallback.ContainsKey(this.CurActionName))
			{
				Action action = this.ActionEndCallback.get_Item(this.CurActionName);
				this.ActionEndCallback.Remove(this.CurActionName);
				if (action != null)
				{
					action.Invoke();
				}
			}
			if (callback != null)
			{
				this.ActionEndCallback.Add(actionName, callback);
			}
			this.CurActionName = actionName;
			this.IsFirstPlayActionEnd = false;
			this.GetAnimator().Play(actionName);
		}

		public void MoveTo(Vector3 position, float speed, Action callBack = null)
		{
			Vector3 vector;
			if (!this.FixNavMeshPosition(position, out vector))
			{
				return;
			}
			this.NavAgent.set_enabled(true);
			this.NavAgent.set_speed(speed);
			this.NavAgent.set_radius(0.001f);
			NavMeshPath navMeshPath = new NavMeshPath();
			if (NavMesh.CalculatePath(base.get_gameObject().get_transform().get_position(), vector, -1, navMeshPath))
			{
				base.get_gameObject().get_transform().LookAt(new Vector3(vector.x, base.get_gameObject().get_transform().get_position().y, vector.z));
				this.NavAgent.SetPath(navMeshPath);
				this.NavEndCallBack = callBack;
			}
		}

		public void MoveTo(Vector3 beginPosition, Vector3 endPosition, float speed, Action callBack = null)
		{
			if (!this.SetPosition(beginPosition))
			{
				return;
			}
			this.MoveTo(endPosition, speed, callBack);
		}

		public void ShowShadow(bool isShow, int modelId)
		{
			ShadowController.ShowShadow(0L, base.get_transform(), !isShow, modelId);
		}

		public void PlaySkillImmediate(Skill dataSkill, Action skillEnd)
		{
			this.ResetAll();
			this.mCurrentSkill = dataSkill.id;
			this.m_uiDisplaySkillEnd = skillEnd;
			this.CastSkill(dataSkill);
			if (this.GetAnimator().HasAction(dataSkill.attAction))
			{
				this.PreciseSetAction(dataSkill.attAction);
			}
			else
			{
				this.DoUIDisplaySkillEnd();
				if (this.ModelType == ActorModelType.UI)
				{
					this.ChangeToIdle();
				}
			}
		}

		public void PlayActionImmediate(string actionName, Action actionEnd)
		{
			this.ResetAll();
			this.m_uiDisplaySkillEnd = actionEnd;
			if (this.GetAnimator().HasAction(actionName))
			{
				this.PreciseSetAction(actionName);
			}
			else
			{
				this.DoUIDisplaySkillEnd();
				if (this.ModelType == ActorModelType.UI && actionName != "die")
				{
					this.ChangeToIdle();
				}
			}
		}

		public void ChangeToIdle()
		{
			if (ControllerTool.SplitIsCity(this.GetAnimator()))
			{
				this.PreciseSetAction("idle_city");
			}
			else
			{
				this.PreciseSetAction("idle");
			}
		}

		private void ResetAll()
		{
			this.ResetPosition();
			this.RemoveActionFX();
			this.effectMessageCache.Clear();
		}

		private void ResetPosition()
		{
			base.get_transform().set_localPosition(this.InitLocalPosition);
		}

		private void SetGameObjectLayer()
		{
			LayerSystem.SetGameObjectLayer(base.get_gameObject(), this.ModelLayer, 1);
		}
	}
}
