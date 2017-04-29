using GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XEngineCommand;

namespace XEngineActor
{
	public class ActorMonster : ActorParentContainer<EntityMonster>
	{
		protected GameObject mountGameObject;

		protected string actionStatusPostfix = string.Empty;

		public List<Collider> ImitationCollider = new List<Collider>();

		protected uint introductionTimer;

		protected bool isInIntroduction;

		public GameObject MountGameObject
		{
			get
			{
				return this.mountGameObject;
			}
			set
			{
				this.mountGameObject = value;
			}
		}

		public string ActionStatusPostfix
		{
			get
			{
				return this.actionStatusPostfix;
			}
			set
			{
				this.actionStatusPostfix = value;
			}
		}

		public override string CurActionStatus
		{
			get
			{
				return base.CurActionStatus;
			}
			set
			{
				if (this.CurActionStatus == value)
				{
					return;
				}
				EntityMonster entityMonster = this.GetEntity() as EntityMonster;
				if (entityMonster != null && entityMonster.IsNoumenon)
				{
					base.CurActionStatus = value;
					using (Dictionary<long, EntityParent>.Enumerator enumerator = entityMonster.Compononts.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<long, EntityParent> current = enumerator.get_Current();
							if (current.get_Value() != null)
							{
								if (current.get_Value().Actor)
								{
									current.get_Value().Actor.CurActionStatus = value;
								}
							}
						}
					}
				}
				else if (entityMonster != null && entityMonster.IsComponont && !entityMonster.IsNoumenonDead && entityMonster.Noumenon != null && entityMonster.Noumenon.Actor && entityMonster.Noumenon.Actor.CurActionStatus != value)
				{
					entityMonster.Noumenon.Actor.CurActionStatus = value;
				}
				else
				{
					base.CurActionStatus = value;
				}
			}
		}

		public bool IsInIntroduction
		{
			get
			{
				return this.isInIntroduction;
			}
			set
			{
				this.isInIntroduction = value;
				this.UpdateActionSpeed();
			}
		}

		public int IntroductionFactor
		{
			get
			{
				return (!this.IsInIntroduction) ? 1 : 0;
			}
		}

		protected override void Update()
		{
			if (this.GetEntity() == null)
			{
				return;
			}
			this.MoveProcess();
			if (base.NextRotateCountDown > 0f)
			{
				base.NextRotateCountDown -= Time.get_deltaTime();
			}
			else
			{
				base.UpdateSight();
			}
		}

		public override void ClearData()
		{
			TimerHeap.DelTimer(this.introductionTimer);
			base.ClearData();
		}

		public override void ResetController()
		{
			AssetManager.AssetOfControllerManager.SetController(base.FixAnimator, this.GetEntity().FixModelID, true);
		}

		public override void BornAnimationEnd()
		{
			base.BornAnimationEnd();
			AnimationClip[] animationClips = base.FixAnimator.get_runtimeAnimatorController().get_animationClips();
			int i = 0;
			while (i < animationClips.Length)
			{
				if (animationClips[i].get_name() != "born")
				{
					i++;
				}
				else
				{
					if (!Enumerable.Any<AnimationEvent>(animationClips[i].get_events(), (AnimationEvent x) => x.get_functionName() == "TweenCamera"))
					{
						break;
					}
					UIManagerControl.Instance.FakeHideAllUI(false, 7);
					EventDispatcher.Broadcast<Transform>(CameraEvent.BossBornEnd, base.FixTransform);
					break;
				}
			}
		}

		public override void DeadAnimationEnd()
		{
			if ((this.GetEntity() as EntityMonster).IsGolem)
			{
				if (DataReader<Monster>.Get(this.GetEntity().TypeID).disappear > 0 && !this.GetEntity().IsCloseRenderer)
				{
					this.FadeOutDie(delegate
					{
						base.DeadAnimationEnd();
						base.FixGameObject.SetActive(false);
					});
				}
				else
				{
					base.DeadAnimationEnd();
				}
			}
			else if (this.GetEntity().IsCloseRenderer)
			{
				base.DeadAnimationEnd();
			}
			else
			{
				this.FadeOutDie(new Action(base.DeadAnimationEnd));
			}
		}

		protected void FadeOutDie(Action callback)
		{
			base.SetAllCollider(false);
			ShaderEffectUtils.SetFade(this.GetEntity().alphaControls, true, delegate
			{
				this.SetAllCollider(true);
				if (callback != null)
				{
					callback.Invoke();
				}
			});
		}

		public override void DeadToDestroy()
		{
			TimerHeap.DelTimer(this.introductionTimer);
			this.IsInIntroduction = false;
			base.DeadToDestroy();
		}

		public override void InitActionPriorityTable()
		{
			base.ActionPriorityTable.Clear();
			EntityParent.MonsterRankType monsterRank = this.GetEntity().MonsterRank;
			if (monsterRank != EntityParent.MonsterRankType.Elite)
			{
				if (monsterRank != EntityParent.MonsterRankType.Boss)
				{
					for (int i = 0; i < DataReader<ActionMonster>.DataList.get_Count(); i++)
					{
						base.ActionPriorityTable.Add(DataReader<ActionMonster>.DataList.get_Item(i).action, DataReader<ActionMonster>.DataList.get_Item(i).priority);
					}
				}
				else
				{
					for (int j = 0; j < DataReader<ActionBoss>.DataList.get_Count(); j++)
					{
						base.ActionPriorityTable.Add(DataReader<ActionBoss>.DataList.get_Item(j).action, DataReader<ActionBoss>.DataList.get_Item(j).priority);
					}
				}
			}
			else
			{
				for (int k = 0; k < DataReader<ActionElite>.DataList.get_Count(); k++)
				{
					base.ActionPriorityTable.Add(DataReader<ActionElite>.DataList.get_Item(k).action, DataReader<ActionElite>.DataList.get_Item(k).priority);
				}
			}
		}

		public void RelocateAnimator(Animator theAnimator, string postfix = "")
		{
			this.fixAnimator = theAnimator;
			this.ActionStatusPostfix = postfix;
		}

		public override void OnCheckCombo(CheckComboCmd cmd)
		{
			base.IsUnderCombo = true;
		}

		public override bool CanChangeActionTo(string newAction, bool isCheckHitMoving = true, int candidateSkillID = 0, bool isLogOpen = false)
		{
			if (string.IsNullOrEmpty(newAction) || string.IsNullOrEmpty(this.CurActionStatus))
			{
				return true;
			}
			if ((this.GetEntity() as EntityMonster).IsComponont && !string.IsNullOrEmpty(newAction))
			{
				newAction += this.ActionStatusPostfix;
			}
			return !this.GetEntity().IsStatic && (!this.GetEntity().IsFixed || !XUtility.StartsWith(newAction, "run")) && (!isCheckHitMoving || !this.GetEntity().IsHitMoving || ActionStatusName.IsDieAction(newAction)) && (!isCheckHitMoving || !base.IsStraight || ActionStatusName.IsDieAction(newAction) || ActionStatusName.IsHitAction(newAction)) && (!this.GetEntity().IsEndure || !ActionStatusName.IsHitAction(newAction)) && (!this.GetEntity().IsWeak || (!ActionStatusName.IsActionCauseNormalMove(newAction) && !ActionStatusName.IsSpinAction(newAction))) && base.HasActionOrFixAction(newAction) && ((base.IsUnderCombo && base.ActionSkillComboID != 0 && base.ActionSkillComboID == candidateSkillID) || newAction == this.CurActionStatus || base.ActionPriorityTable[newAction] > base.ActionPriorityTable[this.CurActionStatus] || base.IsUnderTermination || ActionStatusName.IsIdleAction(newAction));
		}

		protected override int GetOriginalPriority(string name)
		{
			EntityParent.MonsterRankType monsterRank = this.GetEntity().MonsterRank;
			if (monsterRank != EntityParent.MonsterRankType.Elite)
			{
				if (monsterRank != EntityParent.MonsterRankType.Boss)
				{
					if (DataReader<ActionMonster>.Get(name) != null)
					{
						return DataReader<ActionMonster>.Get(name).priority;
					}
					Debug.LogError("ActionMonster 不存在 " + name);
				}
				else
				{
					if (DataReader<ActionBoss>.Get(name) != null)
					{
						return DataReader<ActionBoss>.Get(name).priority;
					}
					Debug.LogError("ActionBoss 不存在 " + name);
				}
			}
			else
			{
				if (DataReader<ActionElite>.Get(name) != null)
				{
					return DataReader<ActionElite>.Get(name).priority;
				}
				Debug.LogError("ActionElite 不存在 " + name);
			}
			return 0;
		}

		public override bool ChangeAction(string newAction, bool isForceChange = false, bool isBreakChange = true, float extraSpeed = 1f, int candidateSkillID = 0, int candidateSkillComboID = 0, string candidateSkillTag = "")
		{
			if ((this.GetEntity() as EntityMonster).IsComponont && !string.IsNullOrEmpty(newAction))
			{
				newAction += this.ActionStatusPostfix;
			}
			return base.ChangeAction(newAction, isForceChange, isBreakChange, extraSpeed, candidateSkillID, candidateSkillComboID, candidateSkillTag);
		}

		public override void EndAnimationResetToIdle()
		{
			EntityMonster entityMonster = this.GetEntity() as EntityMonster;
			if (entityMonster.IsNoumenon && !entityMonster.IsNoumenonActive)
			{
				using (Dictionary<long, EntityParent>.Enumerator enumerator = entityMonster.AliveComponents.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<long, EntityParent> current = enumerator.get_Current();
						EntityMonster entityMonster2 = current.get_Value() as EntityMonster;
						if (entityMonster2.IsComponontActive)
						{
							if (entityMonster2.Actor)
							{
								entityMonster2.Actor.EndAnimationResetToIdle();
								return;
							}
						}
					}
				}
			}
			base.EndAnimationResetToIdle();
		}

		public override void OnActionStatusEnter(ActionStatusEnterCmd cmd)
		{
			base.OnActionStatusEnter(cmd);
			EventDispatcher.BroadcastAsync<int, EntityMonster, string>("BattleDialogTrigger", 1, this.GetEntity() as EntityMonster, this.CurActionStatus);
		}

		public override void LoadEndResetPoistion()
		{
			if (!(this.GetEntity() as EntityMonster).IsComponont)
			{
				base.LoadEndResetPoistion();
			}
		}

		public override bool MoveToSkillTarget(float skillReach)
		{
			if (this.GetEntity().AITarget == null)
			{
				return false;
			}
			if (!this.GetEntity().AITarget.Actor)
			{
				return false;
			}
			float num = XUtility.DistanceNoY(base.FixTransform.get_position(), this.GetEntity().AITarget.Actor.FixTransform.get_position()) - (skillReach + XUtility.GetHitRadius(this.GetEntity().AITarget.Actor.FixTransform));
			if (num <= -0.05f)
			{
				base.StopMoveToPoint();
				return false;
			}
			if (this.CurActionStatus != "run" && !this.CanChangeActionTo("run", true, 0, false) && ActionStatusName.IsSkillAction(this.CurActionStatus) && !this.GetEntity().IsMoveCast && !base.IsUnderTermination)
			{
				return false;
			}
			Vector3 vector = new Vector3(this.GetEntity().AITarget.Actor.FixTransform.get_position().x - base.FixTransform.get_position().x, 0f, this.GetEntity().AITarget.Actor.FixTransform.get_position().z - base.FixTransform.get_position().z);
			Vector3 normalized = vector.get_normalized();
			Vector3 aIMoveFixPoint = base.GetAIMoveFixPoint(base.FixTransform.get_position(), this.GetEntity().AITarget.Actor.FixTransform.get_position(), normalized, num, XUtility.GetHitRadius(base.FixTransform));
			this.MoveToPoint(aIMoveFixPoint, 0f, null);
			this.GetEntity().AIToPoint = new XPoint
			{
				position = aIMoveFixPoint
			};
			return true;
		}

		public override void OnTweenCamera(TweenCameraCmd cmd)
		{
			if (CameraRevolve.instance == null)
			{
				return;
			}
			UIManagerControl.Instance.FakeHideAllUI(true, 7);
			if (CamerasMgr.CameraUI)
			{
				CamerasMgr.CameraUI.set_enabled(true);
			}
			else
			{
				Debug.LogError("CamerasMgr.CameraUI: null");
			}
			if (UINodesManager.NormalUIRoot == null)
			{
				Debug.LogError("UINodesManager.NormalUIRoot: null");
			}
			else if (UINodesManager.NormalUIRoot.GetComponent<Canvas>())
			{
				UINodesManager.NormalUIRoot.GetComponent<Canvas>().set_enabled(false);
			}
			else
			{
				Debug.LogError("UINodesManager.NormalUIRoot.GetComponent<Canvas>(): null");
			}
		}

		public override void OnSlowMotionIntroduction(SlowMotionIntroductionCmd cmd)
		{
			this.IsInIntroduction = true;
			((SlowMotionIntroductionUI)UIManagerControl.Instance.OpenUI("SlowMotionIntroductionUI", UINodesManager.TopUIRoot, false, UIType.NonPush)).SetInit(GameDataUtils.GetChineseContent(DataReader<Monster>.Get(this.GetEntity().TypeID).name, false));
			this.introductionTimer = TimerHeap.AddTimer((uint)cmd.time, 0, delegate
			{
				this.IsInIntroduction = false;
				UIManagerControl.Instance.UnLoadUIPrefab("SlowMotionIntroductionUI");
			});
			EventDispatcher.Broadcast<SlowMotionIntroductionCmd, string>("OnSlowMotionIntroductionEvent", cmd, DataReader<Monster>.Get(this.GetEntity().TypeID).namePic);
		}

		public override void OnActivePart(ActivePartCmd cmd)
		{
			string[] array = cmd.args.get_stringParameter().Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(int.Parse(array[i]));
			}
			Dictionary<long, EntityParent> allParts = (this.GetEntity() as EntityMonster).AllParts;
			Debug.Log(Enumerable.Select<EntityParent, int>(allParts.get_Values(), (EntityParent x) => x.TypeID).Pack(" "));
			Debug.Log(list.Pack(" "));
			using (Dictionary<long, EntityParent>.Enumerator enumerator = allParts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<long, EntityParent> current = enumerator.get_Current();
					if (current.get_Value() != null)
					{
						EntityMonster entityMonster = current.get_Value() as EntityMonster;
						if (!list.Contains(entityMonster.TypeID) || ((!entityMonster.IsNoumenon || !entityMonster.IsNoumenonActive) && (!entityMonster.IsComponont || !entityMonster.IsComponontActive)))
						{
							if (entityMonster.IsNoumenon)
							{
								(current.get_Value() as EntityMonster).DeactiveNoumenon();
							}
							else if (entityMonster.IsComponont)
							{
								(current.get_Value() as EntityMonster).DeactiveComponont();
							}
						}
					}
				}
			}
			using (Dictionary<long, EntityParent>.Enumerator enumerator2 = allParts.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<long, EntityParent> current2 = enumerator2.get_Current();
					if (current2.get_Value() != null)
					{
						if (list.Contains(current2.get_Value().TypeID))
						{
							EntityMonster entityMonster2 = current2.get_Value() as EntityMonster;
							if ((!entityMonster2.IsNoumenon || !entityMonster2.IsNoumenonActive) && (!entityMonster2.IsComponont || !entityMonster2.IsComponontActive))
							{
								if (entityMonster2.IsNoumenon)
								{
									(current2.get_Value() as EntityMonster).ActiveNoumenon();
								}
								else if (entityMonster2.IsComponont)
								{
									(current2.get_Value() as EntityMonster).ActiveComponont();
								}
							}
						}
					}
				}
			}
		}

		public override void OnDeactivePart(DeactivePartCmd cmd)
		{
			string[] array = cmd.args.get_stringParameter().Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(int.Parse(array[i]));
			}
			Dictionary<long, EntityParent> dictionary = new Dictionary<long, EntityParent>();
			if (cmd.args.get_intParameter() == 0 && (this.GetEntity() as EntityMonster).IsNoumenon)
			{
				using (Dictionary<long, EntityParent>.Enumerator enumerator = (this.GetEntity() as EntityMonster).AllParts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<long, EntityParent> current = enumerator.get_Current();
						dictionary.Add(current.get_Key(), current.get_Value());
					}
				}
			}
			else
			{
				dictionary.Add(this.GetEntity().ID, this.GetEntity());
			}
			using (Dictionary<long, EntityParent>.Enumerator enumerator2 = dictionary.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<long, EntityParent> current2 = enumerator2.get_Current();
					if (current2.get_Value() != null)
					{
						if (list.Contains(current2.get_Value().TypeID))
						{
							EntityMonster entityMonster = current2.get_Value() as EntityMonster;
							if ((!entityMonster.IsNoumenon || entityMonster.IsNoumenonActive) && (!entityMonster.IsComponont || entityMonster.IsComponontActive))
							{
								if (entityMonster.IsNoumenon)
								{
									(current2.get_Value() as EntityMonster).DeactiveNoumenon();
								}
								else if (entityMonster.IsComponont)
								{
									(current2.get_Value() as EntityMonster).DeactiveComponont();
								}
							}
						}
					}
				}
			}
		}

		public override void OnShowTexture(ShowTextureCmd cmd)
		{
			string[] names = cmd.textureNames.Split(new char[]
			{
				','
			});
			List<GameObject> list = XUtility.RecursiveFindGameObjects(base.FixAnimator.get_gameObject(), names);
			for (int i = 0; i < list.get_Count(); i++)
			{
				list.get_Item(i).SetActive(true);
			}
		}

		public override void OnHideTexture(HideTextureCmd cmd)
		{
			string[] names = cmd.textureNames.Split(new char[]
			{
				','
			});
			List<GameObject> list = XUtility.RecursiveFindGameObjects(base.FixAnimator.get_gameObject(), names);
			for (int i = 0; i < list.get_Count(); i++)
			{
				list.get_Item(i).SetActive(false);
			}
		}

		public override void OnChangeAI(ChangeAICmd cmd)
		{
			int intParameter = cmd.args.get_intParameter();
			string stringParameter = cmd.args.get_stringParameter();
			float floatParameter = cmd.args.get_floatParameter();
			using (Dictionary<long, EntityParent>.Enumerator enumerator = (this.GetEntity() as EntityMonster).AllParts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<long, EntityParent> current = enumerator.get_Current();
					if (current.get_Value() != null)
					{
						if (current.get_Value().TypeID == intParameter)
						{
							current.get_Value().GetAIManager().Deactive();
							current.get_Value().GetAIManager().AIType = stringParameter;
							current.get_Value().GetAIManager().ThinkInterval = (int)floatParameter;
							current.get_Value().GetAIManager().Active();
						}
					}
				}
			}
		}

		public void SetImitationCollider(List<int> colliderType, List<float> localPositions, List<float> localRotations, List<float> localScales)
		{
			for (int i = 0; i < colliderType.get_Count(); i++)
			{
				if (localPositions.get_Count() >= (i + 1) * 3 && localRotations.get_Count() >= (i + 1) * 3 && localScales.get_Count() >= (i + 1) * 3)
				{
					GameObject gameObject = new GameObject();
					gameObject.get_transform().set_parent(base.FixTransform);
					switch (colliderType.get_Item(i))
					{
					case 1:
						this.ImitationCollider.Add(gameObject.AddComponent<BoxCollider>());
						break;
					case 2:
						this.ImitationCollider.Add(gameObject.AddComponent<SphereCollider>());
						break;
					case 3:
						this.ImitationCollider.Add(gameObject.AddComponent<CapsuleCollider>());
						break;
					}
					gameObject.get_transform().set_localPosition(new Vector3(localPositions.get_Item(i * 3), localPositions.get_Item(i * 3 + 1), localPositions.get_Item(i * 3 + 2)));
					gameObject.get_transform().set_localRotation(Quaternion.Euler(new Vector3(localRotations.get_Item(i * 3), localRotations.get_Item(i * 3 + 1), localRotations.get_Item(i * 3 + 2))));
					gameObject.get_transform().set_localScale(new Vector3(localScales.get_Item(i * 3), localScales.get_Item(i * 3 + 1), localScales.get_Item(i * 3 + 2)));
				}
			}
			for (int j = 0; j < this.ImitationCollider.get_Count(); j++)
			{
				this.ImitationCollider.get_Item(j).set_isTrigger(false);
			}
		}

		public override void UpdateActionSpeed()
		{
			base.RealActionSpeed = base.LogicActionSpeed * base.StateActionFactor * base.FrameActionSpeed * base.FrozenActionSpeed * base.StraightActionSpeed * base.TempActionSpeed * (float)this.IntroductionFactor;
			base.PureActionSpeed = base.LogicDefaultActionSpeed * base.FrameActionSpeed * base.TempActionSpeed;
		}
	}
}
