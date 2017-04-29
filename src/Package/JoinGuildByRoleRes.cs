using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(228), ForSend(228), ProtoContract(Name = "JoinGuildByRoleRes")]
	[Serializable]
	public class JoinGuildByRoleRes : IExtensible
	{
		public static readonly short OP = 228;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
