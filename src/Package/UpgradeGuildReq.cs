using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3676), ForSend(3676), ProtoContract(Name = "UpgradeGuildReq")]
	[Serializable]
	public class UpgradeGuildReq : IExtensible
	{
		public static readonly short OP = 3676;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
