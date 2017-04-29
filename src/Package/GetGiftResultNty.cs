using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3124), ForSend(3124), ProtoContract(Name = "GetGiftResultNty")]
	[Serializable]
	public class GetGiftResultNty : IExtensible
	{
		[ProtoContract(Name = "ItemInfo")]
		[Serializable]
		public class ItemInfo : IExtensible
		{
			private int _itemId;

			private long _itemCount;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
			public int itemId
			{
				get
				{
					return this._itemId;
				}
				set
				{
					this._itemId = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "itemCount", DataFormat = DataFormat.TwosComplement)]
			public long itemCount
			{
				get
				{
					return this._itemCount;
				}
				set
				{
					this._itemCount = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 3124;

		private string _key;

		private readonly List<ItemBriefInfo> _items = new List<ItemBriefInfo>();

		private int _code;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.Default)]
		public string key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		[ProtoMember(2, Name = "items", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> items
		{
			get
			{
				return this._items;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "code", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int code
		{
			get
			{
				return this._code;
			}
			set
			{
				this._code = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
