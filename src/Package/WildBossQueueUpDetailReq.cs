using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(155), ForSend(155), ProtoContract(Name = "WildBossQueueUpDetailReq")]
	[Serializable]
	public class WildBossQueueUpDetailReq : IExtensible
	{
		public static readonly short OP = 155;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
