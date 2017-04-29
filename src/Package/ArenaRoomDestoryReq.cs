using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(867), ForSend(867), ProtoContract(Name = "ArenaRoomDestoryReq")]
	[Serializable]
	public class ArenaRoomDestoryReq : IExtensible
	{
		public static readonly short OP = 867;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
