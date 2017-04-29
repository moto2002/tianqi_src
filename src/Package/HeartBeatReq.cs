using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(483), ForSend(483), ProtoContract(Name = "HeartBeatReq")]
	[Serializable]
	public class HeartBeatReq : IExtensible
	{
		public static readonly short OP = 483;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
