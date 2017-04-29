using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(83), ForSend(83), ProtoContract(Name = "RoleMoveRes")]
	[Serializable]
	public class RoleMoveRes : IExtensible
	{
		public static readonly short OP = 83;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
