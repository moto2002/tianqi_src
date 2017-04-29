using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(36), ForSend(36), ProtoContract(Name = "ChallengeExperienceCopyReq")]
	[Serializable]
	public class ChallengeExperienceCopyReq : IExtensible
	{
		public static readonly short OP = 36;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
