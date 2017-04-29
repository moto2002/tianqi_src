using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(84), ForSend(84), ProtoContract(Name = "FlyShoeTransportRes")]
	[Serializable]
	public class FlyShoeTransportRes : IExtensible
	{
		public static readonly short OP = 84;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
