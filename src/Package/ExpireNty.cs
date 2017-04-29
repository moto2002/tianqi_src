using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2223), ForSend(2223), ProtoContract(Name = "ExpireNty")]
	[Serializable]
	public class ExpireNty : IExtensible
	{
		public static readonly short OP = 2223;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
