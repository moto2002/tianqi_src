using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3409), ForSend(3409), ProtoContract(Name = "ExitRoomReq")]
	[Serializable]
	public class ExitRoomReq : IExtensible
	{
		public static readonly short OP = 3409;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
