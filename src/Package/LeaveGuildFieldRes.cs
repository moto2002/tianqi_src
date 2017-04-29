using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(52), ForSend(52), ProtoContract(Name = "LeaveGuildFieldRes")]
	[Serializable]
	public class LeaveGuildFieldRes : IExtensible
	{
		public static readonly short OP = 52;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
