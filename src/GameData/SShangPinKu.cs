using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "SShangPinKu")]
	[Serializable]
	public class SShangPinKu : IExtensible
	{
		private int _Id;

		private int _goodsPool;

		private int _itemId;

		private int _Price;

		private int _vipLv;

		private int _stock;

		private readonly List<int> _discount = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "goodsPool", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int goodsPool
		{
			get
			{
				return this._goodsPool;
			}
			set
			{
				this._goodsPool = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "Price", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				this._Price = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "vipLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vipLv
		{
			get
			{
				return this._vipLv;
			}
			set
			{
				this._vipLv = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "stock", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, Name = "discount", DataFormat = DataFormat.TwosComplement)]
		public List<int> discount
		{
			get
			{
				return this._discount;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
