using AIMind;
using GameData;
using System;
using System.Linq;
using UnityEngine;
using XEngineActor;

namespace EntitySubSystem
{
	public class PlayerAIManager : AIManager
	{
		protected uint pressIconTimer = 4294967295u;

		public override void Active()
		{
			if (base.IsActive)
			{
				return;
			}
			base.MoveSkipThinkCount = 0;
			base.MoveSkipTime = 0f;
			TimerHeap.DelTimer(this.pressIconTimer);
			TimerHeap.DelTimer(this.timerID);
			this.GetAIDataByType();
			base.AIRoot = BTLoader.GetBehaviorTree(this.AIType);
			if (base.AIRoot == null)
			{
				return;
			}
			base.IsActive = true;
			base.IsThinking = InstanceManager.IsAIThinking;
			this.owner.GetConditionManager().RegistThinkCondition(base.GetAIConditionMessage(base.AIRoot));
			this.timerID = TimerHeap.AddTimer(0u, this.ThinkInterval, new Action(this.Think));
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

		protected override void GetAIDataByType()
		{
			if (string.IsNullOrEmpty(this.AIType) || this.ThinkInterval == 0)
			{
				RoleAi roleAi;
				if (this.owner.IsFuse)
				{
					roleAi = Enumerable.FirstOrDefault<RoleAi>(DataReader<RoleAi>.DataList, (RoleAi t) => t.profession == this.owner.TypeID && t.fusePet == (this.owner as EntityPlayer).FusePetID);
				}
				else
				{
					roleAi = Enumerable.FirstOrDefault<RoleAi>(DataReader<RoleAi>.DataList, (RoleAi t) => t.profession == this.owner.TypeID && t.fusePet == 0);
				}
				if (roleAi == null)
				{
					return;
				}
				if (roleAi.otherPlayerAi == null)
				{
					return;
				}
				if (roleAi.otherPlayerAi.get_Count() < 1)
				{
					return;
				}
				this.AIType = roleAi.otherPlayerAi.get_Item(0).key;
				this.ThinkInterval = roleAi.otherPlayerAi.get_Item(0).value;
			}
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
				(this.ownerActor as ActorPlayer).PressSimulateKey(icon);
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
			return this.owner != null && this.owner.GetWarningManager() != null && (!base.ContainAINode(typeof(MoveToEffectOutsideNode)) || !this.owner.GetWarningManager().HasWarningMessage());
		}
	}
}
