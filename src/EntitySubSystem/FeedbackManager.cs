using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EntitySubSystem
{
	public class FeedbackManager : IFeedbackManager, ISubSystem
	{
		protected const string UnknownState = "0";

		protected const char IgnoreState = 'a';

		protected EntityParent owner;

		protected int checkInterval = 3000;

		protected uint timerID;

		protected object thisLock = new object();

		protected bool isActive;

		protected SortedDictionary<FeedbackConditionType, List<FeedbackConditionItem>> feedbackConditionTable = new SortedDictionary<FeedbackConditionType, List<FeedbackConditionItem>>();

		protected Dictionary<FeedbackConditionType, string> feedbackValue = new Dictionary<FeedbackConditionType, string>();

		protected StrategyType curStrategyType;

		protected Array allStrategyType;

		public int CheckInterval
		{
			get
			{
				return this.checkInterval;
			}
			set
			{
				this.checkInterval = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
			set
			{
				this.isActive = value;
			}
		}

		public StrategyType CurStrategyType
		{
			get
			{
				return this.curStrategyType;
			}
			set
			{
				this.curStrategyType = value;
			}
		}

		protected Array AllStrategyType
		{
			get
			{
				return this.allStrategyType;
			}
			set
			{
				this.allStrategyType = value;
			}
		}

		public int AllStrategyTypeCount
		{
			get
			{
				return (this.allStrategyType != null) ? (this.allStrategyType.get_Length() - 1) : 0;
			}
		}

		public void OnCreate(EntityParent theOwner)
		{
			this.owner = theOwner;
			this.IsActive = false;
			this.CurStrategyType = StrategyType.None;
			this.AllStrategyType = Enum.GetValues(typeof(FeedbackConditionType));
			this.RegistFeedbackCondition(DataReader<HuanJingPanDuanBaiFenBi>.DataList);
		}

		public void OnDestroy()
		{
			this.owner = null;
			this.IsActive = false;
			TimerHeap.DelTimer(this.timerID);
		}

		public void Active()
		{
			if (this.IsActive)
			{
				return;
			}
			this.IsActive = true;
			this.timerID = TimerHeap.AddTimer(0u, this.CheckInterval, new Action(this.TryUpdate));
		}

		public void Deactive()
		{
			if (!this.IsActive)
			{
				return;
			}
			this.IsActive = false;
			TimerHeap.DelTimer(this.timerID);
		}

		public void RegistFeedbackCondition(List<HuanJingPanDuanBaiFenBi> temp)
		{
			for (int i = 0; i < temp.get_Count(); i++)
			{
				if (!this.feedbackConditionTable.ContainsKey((FeedbackConditionType)temp.get_Item(i).Type))
				{
					this.feedbackConditionTable.Add((FeedbackConditionType)temp.get_Item(i).Type, new List<FeedbackConditionItem>());
				}
				if (!this.feedbackValue.ContainsKey((FeedbackConditionType)temp.get_Item(i).Type))
				{
					this.feedbackValue.Add((FeedbackConditionType)temp.get_Item(i).Type, string.Empty);
				}
				FeedbackConditionItem feedbackConditionItem = new FeedbackConditionItem();
				feedbackConditionItem.feedbackConditionData = temp.get_Item(i);
				this.feedbackConditionTable.get_Item((FeedbackConditionType)temp.get_Item(i).Type).Add(feedbackConditionItem);
			}
		}

		public void UnregistFeedbackCondition()
		{
			using (SortedDictionary<FeedbackConditionType, List<FeedbackConditionItem>>.Enumerator enumerator = this.feedbackConditionTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<FeedbackConditionType, List<FeedbackConditionItem>> current = enumerator.get_Current();
					current.get_Value().Clear();
				}
			}
			this.feedbackConditionTable.Clear();
		}

		protected void TryUpdate()
		{
			if (!this.IsActive)
			{
				return;
			}
			this.Update();
		}

		protected virtual void Update()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.IsActive)
				{
					this.UpdateAllFeedbackValue();
					this.UpdateStrategyState();
				}
			}
		}

		protected void UpdateAllFeedbackValue()
		{
			using (SortedDictionary<FeedbackConditionType, List<FeedbackConditionItem>>.Enumerator enumerator = this.feedbackConditionTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<FeedbackConditionType, List<FeedbackConditionItem>> current = enumerator.get_Current();
					this.UpdateFeedbackValue(current.get_Key(), current.get_Value());
				}
			}
		}

		protected void UpdateFeedbackValue(FeedbackConditionType type, List<FeedbackConditionItem> dataList)
		{
			float percentage = 0f;
			switch (type)
			{
			case FeedbackConditionType.FightingDifferencePercentage:
				percentage = (float)(this.owner.Fighting - EntityWorld.Instance.EntSelf.Fighting) * 100f / (float)this.owner.Fighting;
				break;
			case FeedbackConditionType.DamagePercentage:
				percentage = (float)(this.owner.RealHpLmt - this.owner.Hp) * 100f / (float)this.owner.RealHpLmt;
				break;
			case FeedbackConditionType.AvatarDamagePercentage:
				percentage = (float)(EntityWorld.Instance.EntSelf.RealHpLmt - EntityWorld.Instance.EntSelf.Hp) * 100f / (float)EntityWorld.Instance.EntSelf.RealHpLmt;
				break;
			case FeedbackConditionType.BossDamagePercentage:
			{
				float num = 0f;
				float num2 = 0f;
				List<EntityParent> values = EntityWorld.Instance.GetEntities<EntityMonster>().Values;
				for (int i = 0; i < values.get_Count(); i++)
				{
					if (values.get_Item(i).IsLogicBoss)
					{
						num += (float)values.get_Item(i).Hp;
						num2 += (float)values.get_Item(i).RealHpLmt;
					}
				}
				percentage = ((num2 != 0f) ? ((num2 - num) * 100f / num2) : 100f);
				break;
			}
			case FeedbackConditionType.RemainTime:
				percentage = (float)InstanceManager.LeftTimePercentage;
				break;
			case FeedbackConditionType.OutPutBossDamagePercentage:
				percentage = InstanceManager.GetPlayerBossDamagePercentage(this.owner.ID);
				break;
			case FeedbackConditionType.FrameDamagePercentage:
				percentage = InstanceManager.GetPlayerFramedPercentage(this.owner.ID);
				break;
			}
			for (int j = 0; j < dataList.get_Count(); j++)
			{
				if (this.CheckFeedbackConditionDetail(percentage, dataList.get_Item(j).feedbackConditionData.percentage))
				{
					this.feedbackValue.set_Item(type, dataList.get_Item(j).feedbackConditionData.value.ToString());
					return;
				}
			}
			this.feedbackValue.set_Item(type, "0");
		}

		protected bool CheckFeedbackConditionDetail(float percentage, string comparison)
		{
			if (comparison == null)
			{
				return true;
			}
			string text = comparison.Replace(" ", string.Empty);
			if (text.Contains("and"))
			{
				string[] array = text.Split("and".ToCharArray());
				if (array.Length > 3)
				{
					return EntityWorld.Instance.CompareFilter(array[0], percentage) && EntityWorld.Instance.CompareFilter(array[3], percentage);
				}
			}
			else
			{
				if (!text.Contains("or"))
				{
					return EntityWorld.Instance.CompareFilter(text, percentage);
				}
				string[] array2 = text.Split("or".ToCharArray());
				if (array2.Length > 3)
				{
					return EntityWorld.Instance.CompareFilter(array2[0], percentage) && EntityWorld.Instance.CompareFilter(array2[3], percentage);
				}
			}
			return true;
		}

		protected void UpdateStrategyState()
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerator enumerator = this.AllStrategyType.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					FeedbackConditionType feedbackConditionType = (FeedbackConditionType)((int)enumerator.get_Current());
					if (this.feedbackValue.ContainsKey(feedbackConditionType))
					{
						stringBuilder.Append(this.feedbackValue.get_Item(feedbackConditionType));
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			string text = stringBuilder.ToString();
			CeLveZu ceLveZu = (!DataReader<CeLveZu>.Contains(text)) ? null : DataReader<CeLveZu>.Get(text);
			if (ceLveZu == null)
			{
				int num = -1;
				for (int i = 0; i < DataReader<CeLveZu>.DataList.get_Count(); i++)
				{
					int num2 = this.CheckIsStrategyMatch(text, DataReader<CeLveZu>.DataList.get_Item(i).StrategyId);
					if (num2 > num)
					{
						ceLveZu = DataReader<CeLveZu>.DataList.get_Item(i);
						num = num2;
					}
				}
			}
			if (ceLveZu != null && ceLveZu.StrategyPercentage.get_Count() <= this.AllStrategyTypeCount)
			{
				this.SetStrategyState(ceLveZu);
			}
		}

		protected int CheckIsStrategyMatch(string input, string pattern)
		{
			int num = 0;
			for (int i = 0; i < input.get_Length(); i++)
			{
				if (pattern.get_Length() <= i)
				{
					return -1;
				}
				if (pattern.get_Chars(i) != input.get_Chars(i) && pattern.get_Chars(i) != 'a' && char.ToLower(pattern.get_Chars(i)) != 'a')
				{
					return -1;
				}
				if (pattern.get_Chars(i) == input.get_Chars(i))
				{
					num++;
				}
			}
			return num;
		}

		protected void SetStrategyState(CeLveZu strategyData)
		{
			int num = 0;
			for (int i = 0; i < strategyData.StrategyPercentage.get_Count(); i++)
			{
				num += strategyData.StrategyPercentage.get_Item(i);
			}
			if (num == 0)
			{
				this.CurStrategyType = StrategyType.None;
				return;
			}
			int num2 = Random.Range(0, num);
			int num3 = 0;
			for (int j = 0; j < strategyData.StrategyPercentage.get_Count(); j++)
			{
				num3 += strategyData.StrategyPercentage.get_Item(j);
				if (num2 < num3)
				{
					this.CurStrategyType = j + StrategyType.Wander;
					return;
				}
			}
			this.CurStrategyType = StrategyType.None;
		}
	}
}
