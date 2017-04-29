using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3889), ForSend(3889), ProtoContract(Name = "QueryGuildShopInfoReq")]
	[Serializable]
	public class QueryGuildShopInfoReq : IExtensible
	{
		public static readonly short OP = 3889;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
