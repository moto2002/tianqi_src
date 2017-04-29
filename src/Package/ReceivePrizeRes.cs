using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(657), ForSend(657), ProtoContract(Name = "ReceivePrizeRes")]
	[Serializable]
	public class ReceivePrizeRes : IExtensible
	{
		public static readonly short OP = 657;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
