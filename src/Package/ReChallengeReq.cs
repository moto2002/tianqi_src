using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3100), ForSend(3100), ProtoContract(Name = "ReChallengeReq")]
	[Serializable]
	public class ReChallengeReq : IExtensible
	{
		public static readonly short OP = 3100;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
