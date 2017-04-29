using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(131), ForSend(131), ProtoContract(Name = "GetFundDiamondReq")]
	[Serializable]
	public class GetFundDiamondReq : IExtensible
	{
		public static readonly short OP = 131;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
