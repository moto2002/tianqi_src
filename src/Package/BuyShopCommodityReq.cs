using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1681), ForSend(1681), ProtoContract(Name = "BuyShopCommodityReq")]
	[Serializable]
	public class BuyShopCommodityReq : IExtensible
	{
		public static readonly short OP = 1681;

		private int _shopId;

		private int _commodityId;

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

		[ProtoMember(2, IsRequired = true, Name = "commodityId", DataFormat = DataFormat.TwosComplement)]
		public int commodityId
		{
			get
			{
				return this._commodityId;
			}
			set
			{
				this._commodityId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
