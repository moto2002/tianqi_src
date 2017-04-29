using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(841), ForSend(841), ProtoContract(Name = "EnterWaitingRoomReq")]
	[Serializable]
	public class EnterWaitingRoomReq : IExtensible
	{
		public static readonly short OP = 841;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
