using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(791), ForSend(791), ProtoContract(Name = "ExitElementCopyRes")]
	[Serializable]
	public class ExitElementCopyRes : IExtensible
	{
		public static readonly short OP = 791;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
