using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3721), ForSend(3721), ProtoContract(Name = "ExitProtectFightRes")]
	[Serializable]
	public class ExitProtectFightRes : IExtensible
	{
		public static readonly short OP = 3721;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
