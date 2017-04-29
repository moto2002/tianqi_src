using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4897), ForSend(4897), ProtoContract(Name = "CostRefreshGuildShopReq")]
	[Serializable]
	public class CostRefreshGuildShopReq : IExtensible
	{
		public static readonly short OP = 4897;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
