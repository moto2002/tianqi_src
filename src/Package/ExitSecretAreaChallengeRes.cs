using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(925), ForSend(925), ProtoContract(Name = "ExitSecretAreaChallengeRes")]
	[Serializable]
	public class ExitSecretAreaChallengeRes : IExtensible
	{
		public static readonly short OP = 925;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
