using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleSkillInfo")]
	[Serializable]
	public class BattleSkillInfo : IExtensible
	{
		private int _skillId;

		private int _skillIdx;

		private int _skillLv = 1;

		private readonly List<BattleSkillAttrAdd> _attrAdd = new List<BattleSkillAttrAdd>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "skillIdx", DataFormat = DataFormat.TwosComplement)]
		public int skillIdx
		{
			get
			{
				return this._skillIdx;
			}
			set
			{
				this._skillIdx = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "skillLv", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int skillLv
		{
			get
			{
				return this._skillLv;
			}
			set
			{
				this._skillLv = value;
			}
		}

		[ProtoMember(4, Name = "attrAdd", DataFormat = DataFormat.Default)]
		public List<BattleSkillAttrAdd> attrAdd
		{
			get
			{
				return this._attrAdd;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
