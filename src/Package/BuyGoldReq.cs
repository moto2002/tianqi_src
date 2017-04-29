using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(415), ForSend(415), ProtoContract(Name = "BuyGoldReq")]
	[Serializable]
	public class BuyGoldReq : IExtensible
	{
		public static readonly short OP = 415;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
