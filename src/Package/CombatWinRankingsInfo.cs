using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(906), ForSend(906), ProtoContract(Name = "CombatWinRankingsInfo")]
	[Serializable]
	public class CombatWinRankingsInfo : IExtensible
	{
		public static readonly short OP = 906;

		private readonly List<CombatWinRankingsItem> _items = new List<CombatWinRankingsItem>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<CombatWinRankingsItem> items
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
