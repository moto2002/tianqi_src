using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "InviteSetting")]
	[Serializable]
	public class InviteSetting : IExtensible
	{
		private int _roleMinLv;

		private bool _available;

		private bool _verify;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleMinLv", DataFormat = DataFormat.TwosComplement)]
		public int roleMinLv
		{
			get
			{
				return this._roleMinLv;
			}
			set
			{
				this._roleMinLv = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "available", DataFormat = DataFormat.Default)]
		public bool available
		{
			get
			{
				return this._available;
			}
			set
			{
				this._available = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "verify", DataFormat = DataFormat.Default)]
		public bool verify
		{
			get
			{
				return this._verify;
			}
			set
			{
				this._verify = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
