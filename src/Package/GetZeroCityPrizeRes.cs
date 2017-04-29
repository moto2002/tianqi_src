using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3368), ForSend(3368), ProtoContract(Name = "GetZeroCityPrizeRes")]
	[Serializable]
	public class GetZeroCityPrizeRes : IExtensible
	{
		public static readonly short OP = 3368;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
