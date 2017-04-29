using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1121), ForSend(1121), ProtoContract(Name = "PurchaseSecretAreaTimesRes")]
	[Serializable]
	public class PurchaseSecretAreaTimesRes : IExtensible
	{
		public static readonly short OP = 1121;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
