using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(50), ForSend(50), ProtoContract(Name = "LeaveGuildFieldReq")]
	[Serializable]
	public class LeaveGuildFieldReq : IExtensible
	{
		public static readonly short OP = 50;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
