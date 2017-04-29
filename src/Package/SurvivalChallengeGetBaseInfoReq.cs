using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2090), ForSend(2090), ProtoContract(Name = "SurvivalChallengeGetBaseInfoReq")]
	[Serializable]
	public class SurvivalChallengeGetBaseInfoReq : IExtensible
	{
		public static readonly short OP = 2090;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
