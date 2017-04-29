using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4147), ForSend(4147), ProtoContract(Name = "AbandonTaskRes")]
	[Serializable]
	public class AbandonTaskRes : IExtensible
	{
		public static readonly short OP = 4147;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
