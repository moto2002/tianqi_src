using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(295), ForSend(295), ProtoContract(Name = "PingReq")]
	[Serializable]
	public class PingReq : IExtensible
	{
		public static readonly short OP = 295;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
