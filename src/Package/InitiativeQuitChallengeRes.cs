using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1148), ForSend(1148), ProtoContract(Name = "InitiativeQuitChallengeRes")]
	[Serializable]
	public class InitiativeQuitChallengeRes : IExtensible
	{
		public static readonly short OP = 1148;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
