using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2181), ForSend(2181), ProtoContract(Name = "GetGuildWarChampionDailyPrizeRes")]
	[Serializable]
	public class GetGuildWarChampionDailyPrizeRes : IExtensible
	{
		public static readonly short OP = 2181;

		private readonly List<ItemBriefInfo> _itemInfo = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "itemInfo", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> itemInfo
		{
			get
			{
				return this._itemInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
