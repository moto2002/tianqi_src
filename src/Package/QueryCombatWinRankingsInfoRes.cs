using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1276), ForSend(1276), ProtoContract(Name = "QueryCombatWinRankingsInfoRes")]
	[Serializable]
	public class QueryCombatWinRankingsInfoRes : IExtensible
	{
		public static readonly short OP = 1276;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
