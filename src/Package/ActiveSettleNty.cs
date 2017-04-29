using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(160), ForSend(160), ProtoContract(Name = "ActiveSettleNty")]
	[Serializable]
	public class ActiveSettleNty : IExtensible
	{
		public static readonly short OP = 160;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
