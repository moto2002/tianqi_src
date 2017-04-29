using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(530), ForSend(530), ProtoContract(Name = "KickMemberReq")]
	[Serializable]
	public class KickMemberReq : IExtensible
	{
		public static readonly short OP = 530;

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
