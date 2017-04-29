using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(566), ForSend(566), ProtoContract(Name = "CommitTaskRes")]
	[Serializable]
	public class CommitTaskRes : IExtensible
	{
		public static readonly short OP = 566;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
