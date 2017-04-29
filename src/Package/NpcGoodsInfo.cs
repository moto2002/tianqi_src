using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "NpcGoodsInfo")]
	[Serializable]
	public class NpcGoodsInfo : IExtensible
	{
		private int _libIndex;

		private int _itemId;

		private int _itemType;

		private int _itemCount;

		private int _stock;

		private int _reputation;

		private readonly List<NpcExchangeInfo> _exchangeInfo = new List<NpcExchangeInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "libIndex", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int libIndex
		{
			get
			{
				return this._libIndex;
			}
			set
			{
				this._libIndex = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "itemType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				this._itemType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "itemCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemCount
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

		[ProtoMember(5, IsRequired = false, Name = "stock", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int stock
		{
			get
			{
				return this._stock;
			}
			set
			{
				this._stock = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "reputation", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reputation
		{
			get
			{
				return this._reputation;
			}
			set
			{
				this._reputation = value;
			}
		}

		[ProtoMember(7, Name = "exchangeInfo", DataFormat = DataFormat.Default)]
		public List<NpcExchangeInfo> exchangeInfo
		{
			get
			{
				return this._exchangeInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
