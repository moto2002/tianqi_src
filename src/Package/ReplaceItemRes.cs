using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6813), ForSend(6813), ProtoContract(Name = "ReplaceItemRes")]
	[Serializable]
	public class ReplaceItemRes : IExtensible
	{
		public static readonly short OP = 6813;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
