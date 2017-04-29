using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6807), ForSend(6807), ProtoContract(Name = "DiscountItemsLoginPush")]
	[Serializable]
	public class DiscountItemsLoginPush : IExtensible
	{
		public static readonly short OP = 6807;

		private int _coupons;

		private float _countdown;

		private readonly List<DiscountItemsInfo> _itemsInfo = new List<DiscountItemsInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "coupons", DataFormat = DataFormat.TwosComplement)]
		public int coupons
		{
			get
			{
				return this._coupons;
			}
			set
			{
				this._coupons = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "countdown", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float countdown
		{
			get
			{
				return this._countdown;
			}
			set
			{
				this._countdown = value;
			}
		}

		[ProtoMember(2, Name = "itemsInfo", DataFormat = DataFormat.Default)]
		public List<DiscountItemsInfo> itemsInfo
		{
			get
			{
				return this._itemsInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
