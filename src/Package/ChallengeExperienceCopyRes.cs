using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(37), ForSend(37), ProtoContract(Name = "ChallengeExperienceCopyRes")]
	[Serializable]
	public class ChallengeExperienceCopyRes : IExtensible
	{
		public static readonly short OP = 37;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
