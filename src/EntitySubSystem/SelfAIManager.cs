using AIMind;
using GameData;
using System;
using System.Linq;
using UnityEngine;

namespace EntitySubSystem
{
	public class SelfAIManager : AIManager
	{
		protected uint pressIconTimer = 4294967295u;

		protected override void AddListeners()
		{
			EventDispatcher.AddListener(AIManagerEvent.ResetSelfAI, new Callback(this.ResetSelfAI));
			EventDispatcher.AddListener(AIManagerEvent.SelfAIActive, new Callback(this.Active));
			EventDispatcher.AddListener(AIManagerEvent.SelfAIDeactive, new Callback(this.Deactive));
			base.AddListeners();
		}

		protected override void RemoveListners()
		{
			EventDispatcher.RemoveListener(AIManagerEvent.ResetSelfAI, new Callback(this.ResetSelfAI));
			EventDispatcher.RemoveListener(AIManagerEvent.SelfAIActive, new Callback(this.Active));
			EventDispatcher.RemoveListener(AIManagerEvent.SelfAIDeactive, new Callback(this.Deactive));
			base.RemoveListners();
		}

		public override void Active()
		{
			if (base.IsActive)
			{
				return;
			}
			base.MoveSkipThinkCount = 0;
			base.MoveSkipTime = 0f;
			TimerHeap.DelTimer(this.pressIconTimer);
			base.Active();
		}

		public override void Deactive()
		{
			if (!base.IsActive)
			{
				return;
			}
			base.MoveSkipThinkCount = 0;
			base.MoveSkipTime = 0f;
			TimerHeap.DelTimer(this.pressIconTimer);
			base.Deactive();
		}

		protected void ResetSelfAI()
		{
			this.AIType = string.Empty;
			this.ThinkInterval = 0;
		}

		protected override void GetAIDataByType()
		{
			if (string.IsNullOrEmpty(this.AIType) || this.ThinkInterval == 0)
			{
				RoleAi roleAi;
				if (this.owner.IsFuse)
				{
					roleAi = Enumerable.FirstOrDefault<RoleAi>(DataReader<RoleAi>.DataList, (RoleAi t) => t.profession == this.owner.TypeID && t.fusePet == (this.owner as EntitySelf).FusePetID);
				}
				else
				{
					roleAi = Enumerable.FirstOrDefault<RoleAi>(DataReader<RoleAi>.DataList, (RoleAi t) => t.profession == this.owner.TypeID && t.fusePet == 0);
				}
				if (roleAi == null)
				{
					return;
				}
				if (roleAi.ai == null)
				{
					return;
				}
				if (roleAi.ai.get_Count() < 1)
				{
					return;
				}
				this.AIType = roleAi.ai.get_Item(0).key;
				this.ThinkInterval = roleAi.ai.get_Item(0).value;
			}
		}

		public override bool CheckSelf()
		{
			if (!GlobalBattleNetwork.Instance.IsServerEnable)
			{
				if (this.ownerActor)
				{
					this.ownerActor.IsClearTargetPosition = true;
				}
				return false;
			}
			return base.CheckSelf();
		}

		public override bool MoveToEffectOutside(int thinkCount)
		{
			if (this.owner.IsWeak)
			{
				return false;
			}
			if (!this.ownerActor)
			{
				return false;
			}
			if (this.owner.GetWarningManager().ExecuteWarningMessage(null, null))
			{
				TimerHeap.DelTimer(this.pressIconTimer);
				base.MoveSkipThinkCount = thinkCount;
				return true;
			}
			return false;
		}

		public override bool PressIcon(int icon, int count, int interval)
		{
			if (!this.CheckCanContinuePressIcon())
			{
				return false;
			}
			if (this.owner.AITarget != null && this.owner.AITarget.Actor)
			{
				this.ownerActor.ForceSetDirection(new Vector3(this.owner.AITarget.Actor.FixTransform.get_position().x - this.ownerActor.FixTransform.get_position().x, 0f, this.owner.AITarget.Actor.FixTransform.get_position().z - this.ownerActor.FixTransform.get_position().z));
				this.ownerActor.ApplyMovingDirAsForward();
			}
			if (!this.owner.IsAssault)
			{
				XInputManager.Instance.OnSkillBtnDown(icon, false);
			}
			count--;
			if (count > 0 && base.IsActive)
			{
				this.pressIconTimer = TimerHeap.AddTimer((uint)interval, 0, delegate
				{
					this.PressIcon(icon, count, interval);
				});
			}
			return true;
		}

		protected bool CheckCanContinuePressIcon()
		{
			return this.owner != null && this.owner.GetWarningManager() != null && (!base.ContainAINode(typeof(MoveToEffectOutsideNode)) || !this.owner.GetWarningManager().HasWarningMessage()) && !this.owner.IsStatic && !this.owner.IsDizzy && !this.owner.IsWeak;
		}
	}
}
