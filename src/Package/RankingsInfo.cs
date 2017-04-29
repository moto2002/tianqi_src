using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(527), ForSend(527), ProtoContract(Name = "RankingsInfo")]
	[Serializable]
	public class RankingsInfo : IExtensible
	{
		public static readonly short OP = 527;

		private readonly List<RankingsItem> _items = new List<RankingsItem>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<RankingsItem> items
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
