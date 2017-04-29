using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7765), ForSend(7765), ProtoContract(Name = "GetWingDetailReq")]
	[Serializable]
	public class GetWingDetailReq : IExtensible
	{
		public static readonly short OP = 7765;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
