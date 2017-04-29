using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2100), ForSend(2100), ProtoContract(Name = "DarkTrainChallengeReq")]
	[Serializable]
	public class DarkTrainChallengeReq : IExtensible
	{
		public static readonly short OP = 2100;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
