using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(944), ForSend(944), ProtoContract(Name = "TimeLimitedSalesBuyRes")]
	[Serializable]
	public class TimeLimitedSalesBuyRes : IExtensible
	{
		public static readonly short OP = 944;

		private long _goodsNumber;

		private int _remainBuyCount = -1;

		private int _itemId;

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

		[ProtoMember(2, IsRequired = false, Name = "remainBuyCount", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
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

		[ProtoMember(3, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
