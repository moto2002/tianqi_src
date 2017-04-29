using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1158), ForSend(1158), ProtoContract(Name = "ClientDrvUpdateWeakStateRes")]
	[Serializable]
	public class ClientDrvUpdateWeakStateRes : IExtensible
	{
		public static readonly short OP = 1158;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
