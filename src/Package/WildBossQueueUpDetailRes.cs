using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(156), ForSend(156), ProtoContract(Name = "WildBossQueueUpDetailRes")]
	[Serializable]
	public class WildBossQueueUpDetailRes : IExtensible
	{
		public static readonly short OP = 156;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
