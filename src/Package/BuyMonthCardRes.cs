using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4932), ForSend(4932), ProtoContract(Name = "BuyMonthCardRes")]
	[Serializable]
	public class BuyMonthCardRes : IExtensible
	{
		public static readonly short OP = 4932;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
