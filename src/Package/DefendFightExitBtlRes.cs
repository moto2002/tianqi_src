using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(864), ForSend(864), ProtoContract(Name = "DefendFightExitBtlRes")]
	[Serializable]
	public class DefendFightExitBtlRes : IExtensible
	{
		public static readonly short OP = 864;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
