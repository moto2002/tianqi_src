using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "GetShopInfos")]
	[Serializable]
	public class GetShopInfos : IExtensible
	{
		private int _shopId;

		private readonly List<CommodityInfo> _commodities = new List<CommodityInfo>();

		private int _remainingRefreshTime;

		private int _openLv;

		private int _remainRefresh;

		private int _useRefresh;

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

		[ProtoMember(3, IsRequired = true, Name = "remainingRefreshTime", DataFormat = DataFormat.TwosComplement)]
		public int remainingRefreshTime
		{
			get
			{
				return this._remainingRefreshTime;
			}
			set
			{
				this._remainingRefreshTime = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "openLv", DataFormat = DataFormat.TwosComplement)]
		public int openLv
		{
			get
			{
				return this._openLv;
			}
			set
			{
				this._openLv = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "remainRefresh", DataFormat = DataFormat.TwosComplement)]
		public int remainRefresh
		{
			get
			{
				return this._remainRefresh;
			}
			set
			{
				this._remainRefresh = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "useRefresh", DataFormat = DataFormat.TwosComplement)]
		public int useRefresh
		{
			get
			{
				return this._useRefresh;
			}
			set
			{
				this._useRefresh = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
