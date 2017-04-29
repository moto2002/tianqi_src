using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3808), ForSend(3808), ProtoContract(Name = "MapTransportRes")]
	[Serializable]
	public class MapTransportRes : IExtensible
	{
		public static readonly short OP = 3808;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
