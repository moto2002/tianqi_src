using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(47), ForSend(47), ProtoContract(Name = "MemoryFlopOpenUIReq")]
	[Serializable]
	public class MemoryFlopOpenUIReq : IExtensible
	{
		public static readonly short OP = 47;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
