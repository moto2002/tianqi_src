using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(921), ForSend(921), ProtoContract(Name = "PurchaseSkillPointReq")]
	[Serializable]
	public class PurchaseSkillPointReq : IExtensible
	{
		public static readonly short OP = 921;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
