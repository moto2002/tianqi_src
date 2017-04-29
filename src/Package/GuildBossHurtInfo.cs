using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildBossHurtInfo")]
	[Serializable]
	public class GuildBossHurtInfo : IExtensible
	{
		private long _roleId;

		private string _roleName;

		private long _hurtValue;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "roleName", DataFormat = DataFormat.Default)]
		public string roleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				this._roleName = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "hurtValue", DataFormat = DataFormat.TwosComplement)]
		public long hurtValue
		{
			get
			{
				return this._hurtValue;
			}
			set
			{
				this._hurtValue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
