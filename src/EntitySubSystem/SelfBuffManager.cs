using GameData;
using System;

namespace EntitySubSystem
{
	public class SelfBuffManager : BuffManager
	{
		public override bool AddBuff(int buffID, int time)
		{
			Buff buffByID = base.GetBuffByID(buffID);
			bool flag = this.HasBuff(buffID);
			if (base.AddBuff(buffID, time))
			{
				if (!flag)
				{
					this.CheckFloatText(buffByID);
				}
				return true;
			}
			return false;
		}

		protected void CheckFloatText(Buff buffData)
		{
			if (buffData.text == 0)
			{
				return;
			}
			BuffType type = (BuffType)buffData.type;
			if (type != BuffType.ChangeAttrs)
			{
				FloatTipManager.Instance.AddFloatTip(this.owner.ID, this.owner.Actor.FixTransform, GameDataUtils.GetChineseContent(buffData.text, false), string.Empty, true, 2.5f, 1f, 1f, 150f);
			}
			else
			{
				Attrs attrs = DataReader<Attrs>.Get(buffData.targetPropId);
				if (attrs != null)
				{
					for (int i = 0; i < attrs.attrs.get_Count(); i++)
					{
						FloatTipManager.Instance.AddFloatTip(this.owner.ID, this.owner.Actor.FixTransform, string.Format(GameDataUtils.GetChineseContent(buffData.text, false), AttrUtility.GetAttrName((AttrType)attrs.attrs.get_Item(i)), (float)attrs.values.get_Item(i)), string.Empty, true, 2.5f, 1f, 1f, 150f);
					}
				}
			}
		}
	}
}
