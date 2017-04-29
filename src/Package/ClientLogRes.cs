using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1335), ForSend(1335), ProtoContract(Name = "ClientLogRes")]
	[Serializable]
	public class ClientLogRes : IExtensible
	{
		public static readonly short OP = 1335;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
