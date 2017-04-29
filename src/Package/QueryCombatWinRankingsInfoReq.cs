using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1274), ForSend(1274), ProtoContract(Name = "QueryCombatWinRankingsInfoReq")]
	[Serializable]
	public class QueryCombatWinRankingsInfoReq : IExtensible
	{
		public static readonly short OP = 1274;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
