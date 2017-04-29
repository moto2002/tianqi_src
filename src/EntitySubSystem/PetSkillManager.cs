using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EntitySubSystem
{
	public class PetSkillManager : SkillManager
	{
		public static int DebutSkillIndex = 4;

		protected float positiveSkillInterval;

		protected DateTime lastPositiveSkillTime;

		public override void OnCreate(EntityParent theOwner)
		{
			base.OnCreate(theOwner);
			this.positiveSkillInterval = DataReader<Pet>.Get(this.owner.TypeID).skillInterval;
		}

		protected override void MarkCD(Skill skillData)
		{
			if (SystemConfig.IsClearCD)
			{
				return;
			}
			DateTime now = DateTime.get_Now();
			if (skillData.type3 == 1)
			{
				this.lastPositiveSkillTime = now;
			}
			this.skillCastTime.set_Item(skillData.id, now);
			for (int i = 0; i < skillData.groupCd.get_Count(); i++)
			{
				if (!this.skillGroupPublicCDAndTime.ContainsKey(skillData.groupCd.get_Item(i).key) || (double)this.skillGroupPublicCDAndTime.get_Item(skillData.groupCd.get_Item(i).key).get_Key() - (now - this.skillGroupPublicCDAndTime.get_Item(skillData.groupCd.get_Item(i).key).get_Value()).get_TotalMilliseconds() <= (double)skillData.groupCd.get_Item(i).value)
				{
					this.skillGroupPublicCDAndTime.set_Item(skillData.groupCd.get_Item(i).key, new KeyValuePair<int, DateTime>(skillData.groupCd.get_Item(i).value, now));
				}
			}
		}

		public override bool CheckSkillInCDByID(int skillID)
		{
			Skill skill = DataReader<Skill>.Get(skillID);
			if (skill == null)
			{
				Debug.LogError("CheckSkillInCDByID: " + skillID);
				return true;
			}
			DateTime now = DateTime.get_Now();
			return (now - this.DebutTime).get_TotalMilliseconds() < (double)skill.initCd || (this.skillCastTime.ContainsKey(skill.id) && (now - this.skillCastTime.get_Item(skill.id)).get_TotalMilliseconds() < (double)(skill.cd + this.owner.GetSkillCDVariationByType(skill.skilltype))) || (this.skillGroupPublicCDAndTime.ContainsKey(skill.group) && (now - this.skillGroupPublicCDAndTime.get_Item(skill.group).get_Value()).get_TotalMilliseconds() < (double)this.skillGroupPublicCDAndTime.get_Item(skill.group).get_Key()) || (skill.type3 == 1 && (now - this.lastPositiveSkillTime).get_TotalMilliseconds() < (double)this.positiveSkillInterval);
		}
	}
}
