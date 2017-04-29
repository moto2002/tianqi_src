using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(42), ForSend(42), ProtoContract(Name = "BuyExperienceCopyBuffRes")]
	[Serializable]
	public class BuyExperienceCopyBuffRes : IExtensible
	{
		public static readonly short OP = 42;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
