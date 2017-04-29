using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DBSkillNotchInfo")]
	[Serializable]
	public class DBSkillNotchInfo : IExtensible
	{
		public static readonly short OP = 630;

		private int _notchId;

		private int _skillNumber;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "notchId", DataFormat = DataFormat.TwosComplement)]
		public int notchId
		{
			get
			{
				return this._notchId;
			}
			set
			{
				this._notchId = value;
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
