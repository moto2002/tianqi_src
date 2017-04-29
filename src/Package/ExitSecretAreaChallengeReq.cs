using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1087), ForSend(1087), ProtoContract(Name = "ExitSecretAreaChallengeReq")]
	[Serializable]
	public class ExitSecretAreaChallengeReq : IExtensible
	{
		public static readonly short OP = 1087;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
