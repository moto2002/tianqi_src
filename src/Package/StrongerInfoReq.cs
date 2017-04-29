using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(674), ForSend(674), ProtoContract(Name = "StrongerInfoReq")]
	[Serializable]
	public class StrongerInfoReq : IExtensible
	{
		public static readonly short OP = 674;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
