using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3410), ForSend(3410), ProtoContract(Name = "ExitRoomRes")]
	[Serializable]
	public class ExitRoomRes : IExtensible
	{
		public static readonly short OP = 3410;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
