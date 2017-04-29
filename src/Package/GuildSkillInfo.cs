using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GuildSkillInfo")]
	[Serializable]
	public class GuildSkillInfo : IExtensible
	{
		private int _skillId;

		private int _skillLv;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "skillId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "skillLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
