using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(824), ForSend(824), ProtoContract(Name = "LeaveWaitingRoomReq")]
	[Serializable]
	public class LeaveWaitingRoomReq : IExtensible
	{
		public static readonly short OP = 824;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
