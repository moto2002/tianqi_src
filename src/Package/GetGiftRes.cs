using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3121), ForSend(3121), ProtoContract(Name = "GetGiftRes")]
	[Serializable]
	public class GetGiftRes : IExtensible
	{
		public static readonly short OP = 3121;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
