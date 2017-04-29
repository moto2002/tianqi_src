using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5631), ForSend(5631), ProtoContract(Name = "GetHitMouseInfoReq")]
	[Serializable]
	public class GetHitMouseInfoReq : IExtensible
	{
		public static readonly short OP = 5631;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
