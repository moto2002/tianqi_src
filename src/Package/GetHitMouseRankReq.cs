using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5117), ForSend(5117), ProtoContract(Name = "GetHitMouseRankReq")]
	[Serializable]
	public class GetHitMouseRankReq : IExtensible
	{
		public static readonly short OP = 5117;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
