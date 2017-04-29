using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1386), ForSend(1386), ProtoContract(Name = "AllDungeonChallengeTimesResetNty")]
	[Serializable]
	public class AllDungeonChallengeTimesResetNty : IExtensible
	{
		public static readonly short OP = 1386;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
