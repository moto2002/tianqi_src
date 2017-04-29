using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1338), ForSend(1338), ProtoContract(Name = "ChatLoginRes")]
	[Serializable]
	public class ChatLoginRes : IExtensible
	{
		public static readonly short OP = 1338;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
