using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(59), ForSend(59), ProtoContract(Name = "WildBossCancelQueueUpReq")]
	[Serializable]
	public class WildBossCancelQueueUpReq : IExtensible
	{
		public static readonly short OP = 59;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
