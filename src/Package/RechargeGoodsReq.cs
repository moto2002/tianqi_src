using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(145), ForSend(145), ProtoContract(Name = "RechargeGoodsReq")]
	[Serializable]
	public class RechargeGoodsReq : IExtensible
	{
		public static readonly short OP = 145;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
