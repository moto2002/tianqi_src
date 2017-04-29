using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1119), ForSend(1119), ProtoContract(Name = "PurchaseSecretAreaTimesReq")]
	[Serializable]
	public class PurchaseSecretAreaTimesReq : IExtensible
	{
		public static readonly short OP = 1119;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
