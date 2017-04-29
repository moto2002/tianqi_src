using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(35), ForSend(35), ProtoContract(Name = "RoleReconnectRes")]
	[Serializable]
	public class RoleReconnectRes : IExtensible
	{
		public static readonly short OP = 35;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
