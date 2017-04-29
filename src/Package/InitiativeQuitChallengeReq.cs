using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1146), ForSend(1146), ProtoContract(Name = "InitiativeQuitChallengeReq")]
	[Serializable]
	public class InitiativeQuitChallengeReq : IExtensible
	{
		public static readonly short OP = 1146;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
