using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(8831), ForSend(8831), ProtoContract(Name = "ActiveCenterPushReq")]
	[Serializable]
	public class ActiveCenterPushReq : IExtensible
	{
		public static readonly short OP = 8831;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
