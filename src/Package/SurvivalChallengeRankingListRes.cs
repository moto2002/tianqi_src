using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1379), ForSend(1379), ProtoContract(Name = "SurvivalChallengeRankingListRes")]
	[Serializable]
	public class SurvivalChallengeRankingListRes : IExtensible
	{
		public static readonly short OP = 1379;

		private readonly List<SCPersonalRankingInfo> _items = new List<SCPersonalRankingInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<SCPersonalRankingInfo> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
