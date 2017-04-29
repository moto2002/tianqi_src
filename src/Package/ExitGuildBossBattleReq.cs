using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(73), ForSend(73), ProtoContract(Name = "ExitGuildBossBattleReq")]
	[Serializable]
	public class ExitGuildBossBattleReq : IExtensible
	{
		public static readonly short OP = 73;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
