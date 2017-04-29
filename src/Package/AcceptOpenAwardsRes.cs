using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(801), ForSend(801), ProtoContract(Name = "AcceptOpenAwardsRes")]
	[Serializable]
	public class AcceptOpenAwardsRes : IExtensible
	{
		public static readonly short OP = 801;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
