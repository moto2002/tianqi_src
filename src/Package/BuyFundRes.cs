using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(424), ForSend(424), ProtoContract(Name = "BuyFundRes")]
	[Serializable]
	public class BuyFundRes : IExtensible
	{
		public static readonly short OP = 424;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
