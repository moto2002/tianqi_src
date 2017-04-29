using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2838), ForSend(2838), ProtoContract(Name = "ShoppingReq")]
	[Serializable]
	public class ShoppingReq : IExtensible
	{
		public static readonly short OP = 2838;

		private ShopType.ST _shopType;

		private int _shopId;

		private int _goodsId;

		private int _count = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "shopType", DataFormat = DataFormat.TwosComplement)]
		public ShopType.ST shopType
		{
			get
			{
				return this._shopType;
			}
			set
			{
				this._shopType = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "shopId", DataFormat = DataFormat.TwosComplement)]
		public int shopId
		{
			get
			{
				return this._shopId;
			}
			set
			{
				this._shopId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "goodsId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int goodsId
		{
			get
			{
				return this._goodsId;
			}
			set
			{
				this._goodsId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
