using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "NpcShopInfo")]
	[Serializable]
	public class NpcShopInfo : IExtensible
	{
		private int _shopId;

		private readonly List<NpcGoodsInfo> _goodsInfo = new List<NpcGoodsInfo>();

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

		[ProtoMember(2, Name = "goodsInfo", DataFormat = DataFormat.Default)]
		public List<NpcGoodsInfo> goodsInfo
		{
			get
			{
				return this._goodsInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
