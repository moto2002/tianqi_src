using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(187), ForSend(187), ProtoContract(Name = "MultiPveSettleNty")]
	[Serializable]
	public class MultiPveSettleNty : IExtensible
	{
		public static readonly short OP = 187;

		private bool _isWin;

		private readonly List<ItemBriefInfo> _items = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "isWin", DataFormat = DataFormat.Default)]
		public bool isWin
		{
			get
			{
				return this._isWin;
			}
			set
			{
				this._isWin = value;
			}
		}

		[ProtoMember(3, Name = "items", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> items
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
