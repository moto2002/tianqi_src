using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(670), ForSend(670), ProtoContract(Name = "OneMoveTimePVPReq")]
	[Serializable]
	public class OneMoveTimePVPReq : IExtensible
	{
		public static readonly short OP = 670;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
