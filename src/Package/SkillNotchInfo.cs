using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "SkillNotchInfo")]
	[Serializable]
	public class SkillNotchInfo : IExtensible
	{
		private int _skillId;

		private int _skillNumber;

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

		[ProtoMember(2, IsRequired = false, Name = "skillNumber", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillNumber
		{
			get
			{
				return this._skillNumber;
			}
			set
			{
				this._skillNumber = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
