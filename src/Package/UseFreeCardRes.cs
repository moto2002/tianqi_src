using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(133), ForSend(133), ProtoContract(Name = "UseFreeCardRes")]
	[Serializable]
	public class UseFreeCardRes : IExtensible
	{
		public static readonly short OP = 133;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
