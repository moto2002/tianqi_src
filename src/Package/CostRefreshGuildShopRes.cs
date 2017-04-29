using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4898), ForSend(4898), ProtoContract(Name = "CostRefreshGuildShopRes")]
	[Serializable]
	public class CostRefreshGuildShopRes : IExtensible
	{
		public static readonly short OP = 4898;

		private readonly List<CommodityInfo> _info = new List<CommodityInfo>();

		private int _shopLv;

		private int _remainingRefreshTime;

		private int _useRefresh;

		private int _refreshLimit;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "info", DataFormat = DataFormat.Default)]
		public List<CommodityInfo> info
		{
			get
			{
				return this._info;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "shopLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int shopLv
		{
			get
			{
				return this._shopLv;
			}
			set
			{
				this._shopLv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "remainingRefreshTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "useRefresh", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "refreshLimit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshLimit
		{
			get
			{
				return this._refreshLimit;
			}
			set
			{
				this._refreshLimit = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
