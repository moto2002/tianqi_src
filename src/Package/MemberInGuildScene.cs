using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MemberInGuildScene")]
	[Serializable]
	public class MemberInGuildScene : IExtensible
	{
		public static readonly short OP = 736;

		private MemberInfo _memberInfo;

		private int _resValue;

		private int _status;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "memberInfo", DataFormat = DataFormat.Default)]
		public MemberInfo memberInfo
		{
			get
			{
				return this._memberInfo;
			}
			set
			{
				this._memberInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "resValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resValue
		{
			get
			{
				return this._resValue;
			}
			set
			{
				this._resValue = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "status", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
