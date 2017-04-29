using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(97), ForSend(97), ProtoContract(Name = "ReliveInGuildWarReq")]
	[Serializable]
	public class ReliveInGuildWarReq : IExtensible
	{
		public static readonly short OP = 97;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
