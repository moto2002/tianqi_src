using GameData;
using System;
using UnityEngine;

namespace EntitySubSystem
{
	public class CityMonsterAIManager : AIManager
	{
		public override void Active()
		{
			TimerHeap.DelTimer(this.timerID);
			this.GetAIDataByType();
			base.AIRoot = BTLoader.GetBehaviorTree(this.AIType);
			if (base.AIRoot == null)
			{
				return;
			}
			base.IsActive = true;
			base.IsThinking = true;
			if (this.owner.GetConditionManager() != null)
			{
				this.owner.GetConditionManager().RegistThinkCondition(base.GetAIConditionMessage(base.AIRoot));
			}
			if (base.IsFirstActive)
			{
				base.IsFirstActive = false;
				this.timerID = TimerHeap.AddTimer((uint)DataReader<Monster>.Get(this.owner.TypeID).aiDelay, this.ThinkInterval, new Action(this.Think));
			}
			else
			{
				this.timerID = TimerHeap.AddTimer(0u, this.ThinkInterval, new Action(this.Think));
			}
		}

		protected override void GetAIDataByType()
		{
			if (string.IsNullOrEmpty(this.AIType) || this.ThinkInterval == 0)
			{
				Monster monster = DataReader<Monster>.Get(this.owner.TypeID);
				int num = Math.Min(monster.aiId.get_Count(), monster.interval.get_Count());
				if (num == 0)
				{
					return;
				}
				int num2 = Random.Range(0, num);
				this.ThinkInterval = monster.interval.get_Item(num2);
				if (this.ThinkInterval == 0)
				{
					this.ThinkInterval = 1000;
				}
			}
		}
	}
}
