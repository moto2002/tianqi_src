using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(738), ForSend(738), ProtoContract(Name = "ReliveInPVPFieldReq")]
	[Serializable]
	public class ReliveInPVPFieldReq : IExtensible
	{
		public static readonly short OP = 738;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
