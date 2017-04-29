using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShangChengBiao")]
	[Serializable]
	public class ShangChengBiao : IExtensible
	{
		private int _shopId;

		private int _system;

		private int _title;

		private int _heading;

		private int _moneyType;

		private readonly List<string> _RefreshTime = new List<string>();

		private int _initiativeRefresh;

		private readonly List<int> _commodityPool = new List<int>();

		private readonly List<int> _commodityQuantity = new List<int>();

		private int _refreshCostType;

		private int _refreshPrice;

		private int _openLevel;

		private readonly List<int> _activeRefreshTime = new List<int>();

		private IExtension extensionObject;

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

		[ProtoMember(3, IsRequired = false, Name = "system", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int system
		{
			get
			{
				return this._system;
			}
			set
			{
				this._system = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "title", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "heading", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int heading
		{
			get
			{
				return this._heading;
			}
			set
			{
				this._heading = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "moneyType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int moneyType
		{
			get
			{
				return this._moneyType;
			}
			set
			{
				this._moneyType = value;
			}
		}

		[ProtoMember(7, Name = "RefreshTime", DataFormat = DataFormat.Default)]
		public List<string> RefreshTime
		{
			get
			{
				return this._RefreshTime;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "initiativeRefresh", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int initiativeRefresh
		{
			get
			{
				return this._initiativeRefresh;
			}
			set
			{
				this._initiativeRefresh = value;
			}
		}

		[ProtoMember(9, Name = "commodityPool", DataFormat = DataFormat.TwosComplement)]
		public List<int> commodityPool
		{
			get
			{
				return this._commodityPool;
			}
		}

		[ProtoMember(10, Name = "commodityQuantity", DataFormat = DataFormat.TwosComplement)]
		public List<int> commodityQuantity
		{
			get
			{
				return this._commodityQuantity;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "refreshCostType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshCostType
		{
			get
			{
				return this._refreshCostType;
			}
			set
			{
				this._refreshCostType = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "refreshPrice", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshPrice
		{
			get
			{
				return this._refreshPrice;
			}
			set
			{
				this._refreshPrice = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "openLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openLevel
		{
			get
			{
				return this._openLevel;
			}
			set
			{
				this._openLevel = value;
			}
		}

		[ProtoMember(14, Name = "activeRefreshTime", DataFormat = DataFormat.TwosComplement)]
		public List<int> activeRefreshTime
		{
			get
			{
				return this._activeRefreshTime;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
