using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1072), ForSend(1072), ProtoContract(Name = "DefendFightBuyChallengeRes")]
	[Serializable]
	public class DefendFightBuyChallengeRes : IExtensible
	{
		public static readonly short OP = 1072;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
