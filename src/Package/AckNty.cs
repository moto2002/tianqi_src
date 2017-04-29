using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1298), ForSend(1298), ProtoContract(Name = "AckNty")]
	[Serializable]
	public class AckNty : IExtensible
	{
		public static readonly short OP = 1298;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
