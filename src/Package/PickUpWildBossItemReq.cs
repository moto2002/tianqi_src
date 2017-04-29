using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(620), ForSend(620), ProtoContract(Name = "PickUpWildBossItemReq")]
	[Serializable]
	public class PickUpWildBossItemReq : IExtensible
	{
		public static readonly short OP = 620;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
