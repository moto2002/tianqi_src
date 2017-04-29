using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(57), ForSend(57), ProtoContract(Name = "SetMessagePushRes")]
	[Serializable]
	public class SetMessagePushRes : IExtensible
	{
		public static readonly short OP = 57;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
