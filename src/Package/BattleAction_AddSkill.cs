using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_AddSkill")]
	[Serializable]
	public class BattleAction_AddSkill : IExtensible
	{
		private long _soldierId;

		private BattleSkillInfo _skillInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "soldierId", DataFormat = DataFormat.TwosComplement)]
		public long soldierId
		{
			get
			{
				return this._soldierId;
			}
			set
			{
				this._soldierId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "skillInfo", DataFormat = DataFormat.Default)]
		public BattleSkillInfo skillInfo
		{
			get
			{
				return this._skillInfo;
			}
			set
			{
				this._skillInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
