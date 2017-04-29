using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2180), ForSend(2180), ProtoContract(Name = "GetGuildWarChampionDailyPrizeReq")]
	[Serializable]
	public class GetGuildWarChampionDailyPrizeReq : IExtensible
	{
		public static readonly short OP = 2180;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
