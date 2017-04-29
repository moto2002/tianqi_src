using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1336), ForSend(1336), ProtoContract(Name = "SurvivalChallengeStartFightRes")]
	[Serializable]
	public class SurvivalChallengeStartFightRes : IExtensible
	{
		public static readonly short OP = 1336;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
