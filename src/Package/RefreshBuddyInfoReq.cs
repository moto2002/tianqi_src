using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(814), ForSend(814), ProtoContract(Name = "RefreshBuddyInfoReq")]
	[Serializable]
	public class RefreshBuddyInfoReq : IExtensible
	{
		public static readonly short OP = 814;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
