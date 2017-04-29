using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BuyGoodsInfo")]
	[Serializable]
	public class BuyGoodsInfo : IExtensible
	{
		private long _goodsNumber;

		private int _remainBuyCount;

		private int _buyCount;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "goodsNumber", DataFormat = DataFormat.TwosComplement)]
		public long goodsNumber
		{
			get
			{
				return this._goodsNumber;
			}
			set
			{
				this._goodsNumber = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "remainBuyCount", DataFormat = DataFormat.TwosComplement)]
		public int remainBuyCount
		{
			get
			{
				return this._remainBuyCount;
			}
			set
			{
				this._remainBuyCount = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "buyCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buyCount
		{
			get
			{
				return this._buyCount;
			}
			set
			{
				this._buyCount = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
