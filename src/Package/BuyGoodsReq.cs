using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3894), ForSend(3894), ProtoContract(Name = "BuyGoodsReq")]
	[Serializable]
	public class BuyGoodsReq : IExtensible
	{
		public static readonly short OP = 3894;

		private int _storeId;

		private int _iId;

		private int _count = 1;

		private int _price;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "storeId", DataFormat = DataFormat.TwosComplement)]
		public int storeId
		{
			get
			{
				return this._storeId;
			}
			set
			{
				this._storeId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "iId", DataFormat = DataFormat.TwosComplement)]
		public int iId
		{
			get
			{
				return this._iId;
			}
			set
			{
				this._iId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
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

		[ProtoMember(4, IsRequired = false, Name = "price", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int price
		{
			get
			{
				return this._price;
			}
			set
			{
				this._price = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
