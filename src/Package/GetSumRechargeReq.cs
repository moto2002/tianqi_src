using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(154), ForSend(154), ProtoContract(Name = "GetSumRechargeReq")]
	[Serializable]
	public class GetSumRechargeReq : IExtensible
	{
		public static readonly short OP = 154;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
