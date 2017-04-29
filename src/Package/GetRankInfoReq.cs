using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3862), ForSend(3862), ProtoContract(Name = "GetRankInfoReq")]
	[Serializable]
	public class GetRankInfoReq : IExtensible
	{
		public static readonly short OP = 3862;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
