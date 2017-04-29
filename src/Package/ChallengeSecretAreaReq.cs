using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(912), ForSend(912), ProtoContract(Name = "ChallengeSecretAreaReq")]
	[Serializable]
	public class ChallengeSecretAreaReq : IExtensible
	{
		public static readonly short OP = 912;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
