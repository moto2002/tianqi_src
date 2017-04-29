using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(698), ForSend(698), ProtoContract(Name = "RoleReconnectReq")]
	[Serializable]
	public class RoleReconnectReq : IExtensible
	{
		public static readonly short OP = 698;

		private string _token;

		private long _roleId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "token", DataFormat = DataFormat.Default)]
		public string token
		{
			get
			{
				return this._token;
			}
			set
			{
				this._token = value;
			}
		}

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
