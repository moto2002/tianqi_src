using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1928), ForSend(1928), ProtoContract(Name = "GetMapUiInfoReq")]
	[Serializable]
	public class GetMapUiInfoReq : IExtensible
	{
		public static readonly short OP = 1928;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
