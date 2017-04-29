using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(612), ForSend(612), ProtoContract(Name = "ExitWildBossRes")]
	[Serializable]
	public class ExitWildBossRes : IExtensible
	{
		public static readonly short OP = 612;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
