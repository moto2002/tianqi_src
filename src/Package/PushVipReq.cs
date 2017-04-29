using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5612), ForSend(5612), ProtoContract(Name = "PushVipReq")]
	[Serializable]
	public class PushVipReq : IExtensible
	{
		public static readonly short OP = 5612;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
