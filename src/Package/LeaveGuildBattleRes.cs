using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(120), ForSend(120), ProtoContract(Name = "LeaveGuildBattleRes")]
	[Serializable]
	public class LeaveGuildBattleRes : IExtensible
	{
		public static readonly short OP = 120;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
