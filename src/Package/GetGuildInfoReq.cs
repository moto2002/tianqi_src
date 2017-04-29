using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(616), ForSend(616), ProtoContract(Name = "GetGuildInfoReq")]
	[Serializable]
	public class GetGuildInfoReq : IExtensible
	{
		public static readonly short OP = 616;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
