using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(64), ForSend(64), ProtoContract(Name = "GuildBossInfoReq")]
	[Serializable]
	public class GuildBossInfoReq : IExtensible
	{
		public static readonly short OP = 64;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
