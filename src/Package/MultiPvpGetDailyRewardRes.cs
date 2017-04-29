using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(185), ForSend(185), ProtoContract(Name = "MultiPvpGetDailyRewardRes")]
	[Serializable]
	public class MultiPvpGetDailyRewardRes : IExtensible
	{
		public static readonly short OP = 185;

		private readonly List<ItemBriefInfo> _rewardList = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "rewardList", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> rewardList
		{
			get
			{
				return this._rewardList;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
