using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(117), ForSend(117), ProtoContract(Name = "LeaveGuildBattleReq")]
	[Serializable]
	public class LeaveGuildBattleReq : IExtensible
	{
		public static readonly short OP = 117;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
