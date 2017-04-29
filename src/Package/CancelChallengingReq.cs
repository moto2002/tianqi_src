using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(829), ForSend(829), ProtoContract(Name = "CancelChallengingReq")]
	[Serializable]
	public class CancelChallengingReq : IExtensible
	{
		public static readonly short OP = 829;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
