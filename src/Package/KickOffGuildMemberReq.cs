using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(852), ForSend(852), ProtoContract(Name = "KickOffGuildMemberReq")]
	[Serializable]
	public class KickOffGuildMemberReq : IExtensible
	{
		public static readonly short OP = 852;

		private long _roleId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
