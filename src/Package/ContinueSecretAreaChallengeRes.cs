using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1280), ForSend(1280), ProtoContract(Name = "ContinueSecretAreaChallengeRes")]
	[Serializable]
	public class ContinueSecretAreaChallengeRes : IExtensible
	{
		public static readonly short OP = 1280;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
