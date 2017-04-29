using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3418), ForSend(3418), ProtoContract(Name = "BuyHookTimeReq")]
	[Serializable]
	public class BuyHookTimeReq : IExtensible
	{
		public static readonly short OP = 3418;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
