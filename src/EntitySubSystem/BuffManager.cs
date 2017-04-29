using GameData;
using System;
using System.Collections.Generic;
using XEngine;
using XEngineCommand;

namespace EntitySubSystem
{
	public class BuffManager : IBuffManager, ISubSystem
	{
		protected EntityParent owner;

		public List<int> buffList = new List<int>();

		public XDict<int, Action> buffEnterAndExitFunc = new XDict<int, Action>();

		private List<int> delBuffIds = new List<int>();

		public void OnCreate(EntityParent theOwner)
		{
			this.owner = theOwner;
			this.buffList.Clear();
		}

		public void OnDestroy()
		{
			this.ClearBuff();
			this.owner = null;
		}

		protected Buff GetBuffByID(int id)
		{
			return DataReader<Buff>.Get(id);
		}

		public virtual bool AddBuff(int buffID, int time)
		{
			Buff buffData = this.GetBuffByID(buffID);
			if (this.owner.Actor)
			{
				this.buffList.Add(buffID);
				CommandCenter.ExecuteCommand(this.owner.Actor.FixTransform, new PlayBuffFXCmd
				{
					fxID = buffData.fx,
					buffID = buffID,
					time = 0,
					scale = DataReader<AvatarModel>.Get(this.owner.FixModelID).fxScale
				});
				if (buffData.action != string.Empty)
				{
					this.owner.Actor.AddStageSuffix(buffData.action);
					Action value = delegate
					{
						this.owner.Actor.RemoveStageSuffix(buffData.action);
					};
					if (this.buffEnterAndExitFunc.ContainsKey(buffID))
					{
						this.buffEnterAndExitFunc[buffID] = value;
					}
					else
					{
						this.buffEnterAndExitFunc.Add(buffID, value);
					}
				}
				return true;
			}
			return false;
		}

		public void RemoveBuff(int buffID)
		{
			if (this.HasBuff(buffID))
			{
				if (this.owner.Actor)
				{
					CommandCenter.ExecuteCommand(this.owner.Actor.FixTransform, new RemoveBuffFXCmd
					{
						buffID = buffID
					});
				}
				this.buffList.Remove(buffID);
				if (this.buffEnterAndExitFunc.ContainsKey(buffID))
				{
					if (this.buffEnterAndExitFunc[buffID] != null)
					{
						this.buffEnterAndExitFunc[buffID].Invoke();
					}
					this.buffEnterAndExitFunc[buffID] = null;
					this.buffEnterAndExitFunc.Remove(buffID);
				}
			}
		}

		public void ClearBuff()
		{
			this.delBuffIds.Clear();
			this.delBuffIds.AddRange(this.buffList);
			for (int i = 0; i < this.delBuffIds.get_Count(); i++)
			{
				this.RemoveBuff(this.delBuffIds.get_Item(i));
			}
		}

		public bool HasTypeBuff(int type)
		{
			for (int i = 0; i < this.buffList.get_Count(); i++)
			{
				if (this.GetBuffByID(this.buffList.get_Item(i)).type == type)
				{
					return true;
				}
			}
			return false;
		}

		public bool HasBuff(int buffID)
		{
			return this.buffList.Contains(buffID);
		}

		public List<int> GetBuffList()
		{
			return this.buffList;
		}
	}
}
