using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(932), ForSend(932), ProtoContract(Name = "ClaimActivityPrizeRes")]
	[Serializable]
	public class ClaimActivityPrizeRes : IExtensible
	{
		public static readonly short OP = 932;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
