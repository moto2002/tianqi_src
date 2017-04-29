using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1131), ForSend(1131), ProtoContract(Name = "BattleReconnCacheConfirmReq")]
	[Serializable]
	public class BattleReconnCacheConfirmReq : IExtensible
	{
		public static readonly short OP = 1131;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
