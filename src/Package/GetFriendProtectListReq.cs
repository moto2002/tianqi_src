using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2921), ForSend(2921), ProtoContract(Name = "GetFriendProtectListReq")]
	[Serializable]
	public class GetFriendProtectListReq : IExtensible
	{
		public static readonly short OP = 2921;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
