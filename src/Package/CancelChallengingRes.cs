using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(831), ForSend(831), ProtoContract(Name = "CancelChallengingRes")]
	[Serializable]
	public class CancelChallengingRes : IExtensible
	{
		public static readonly short OP = 831;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
