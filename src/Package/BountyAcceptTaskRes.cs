using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(833), ForSend(833), ProtoContract(Name = "BountyAcceptTaskRes")]
	[Serializable]
	public class BountyAcceptTaskRes : IExtensible
	{
		public static readonly short OP = 833;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
