using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(67), ForSend(67), ProtoContract(Name = "ChallengeGuildBossReq")]
	[Serializable]
	public class ChallengeGuildBossReq : IExtensible
	{
		public static readonly short OP = 67;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
