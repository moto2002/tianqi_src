using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3772), ForSend(3772), ProtoContract(Name = "SendMailRes")]
	[Serializable]
	public class SendMailRes : IExtensible
	{
		public static readonly short OP = 3772;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
