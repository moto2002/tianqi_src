using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(519), ForSend(519), ProtoContract(Name = "OffLineMsgReq")]
	[Serializable]
	public class OffLineMsgReq : IExtensible
	{
		public static readonly short OP = 519;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
