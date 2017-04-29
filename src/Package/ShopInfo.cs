using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "ShopInfo")]
	[Serializable]
	public class ShopInfo : IExtensible
	{
		private int _shopId;

		private readonly List<CommodityInfo> _commodities = new List<CommodityInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "shopId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, Name = "commodities", DataFormat = DataFormat.Default)]
		public List<CommodityInfo> commodities
		{
			get
			{
				return this._commodities;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
