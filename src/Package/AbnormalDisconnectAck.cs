using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(893), ForSend(893), ProtoContract(Name = "AbnormalDisconnectAck")]
	[Serializable]
	public class AbnormalDisconnectAck : IExtensible
	{
		public static readonly short OP = 893;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
