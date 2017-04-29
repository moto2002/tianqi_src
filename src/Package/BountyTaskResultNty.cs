using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6831), ForSend(6831), ProtoContract(Name = "BountyTaskResultNty")]
	[Serializable]
	public class BountyTaskResultNty : IExtensible
	{
		[ProtoContract(Name = "DropItemInfo")]
		[Serializable]
		public class DropItemInfo : IExtensible
		{
			private int _cfgId;

			private int _count;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "cfgId", DataFormat = DataFormat.TwosComplement)]
			public int cfgId
			{
				get
				{
					return this._cfgId;
				}
				set
				{
					this._cfgId = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "count", DataFormat = DataFormat.TwosComplement)]
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

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 6831;

		private int _win;

		private readonly List<ProductionInfo> _productions = new List<ProductionInfo>();

		private ulong _newProductionUId;

		private int _gotStar;

		private int _totalStar;

		private int _gotScore;

		private int _totalScore;

		private int _ownerToBossHurt;

		private int _oppositeToBossHurt;

		private readonly List<bool> _gotStarCondition = new List<bool>();

		private int _btlCostSec;

		private readonly List<BountyTaskResultNty.DropItemInfo> _items = new List<BountyTaskResultNty.DropItemInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "win", DataFormat = DataFormat.TwosComplement)]
		public int win
		{
			get
			{
				return this._win;
			}
			set
			{
				this._win = value;
			}
		}

		[ProtoMember(2, Name = "productions", DataFormat = DataFormat.Default)]
		public List<ProductionInfo> productions
		{
			get
			{
				return this._productions;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "newProductionUId", DataFormat = DataFormat.TwosComplement), DefaultValue(0f)]
		public ulong newProductionUId
		{
			get
			{
				return this._newProductionUId;
			}
			set
			{
				this._newProductionUId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "gotStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gotStar
		{
			get
			{
				return this._gotStar;
			}
			set
			{
				this._gotStar = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "totalStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int totalStar
		{
			get
			{
				return this._totalStar;
			}
			set
			{
				this._totalStar = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "gotScore", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gotScore
		{
			get
			{
				return this._gotScore;
			}
			set
			{
				this._gotScore = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "totalScore", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int totalScore
		{
			get
			{
				return this._totalScore;
			}
			set
			{
				this._totalScore = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "ownerToBossHurt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ownerToBossHurt
		{
			get
			{
				return this._ownerToBossHurt;
			}
			set
			{
				this._ownerToBossHurt = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "oppositeToBossHurt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int oppositeToBossHurt
		{
			get
			{
				return this._oppositeToBossHurt;
			}
			set
			{
				this._oppositeToBossHurt = value;
			}
		}

		[ProtoMember(10, Name = "gotStarCondition", DataFormat = DataFormat.Default)]
		public List<bool> gotStarCondition
		{
			get
			{
				return this._gotStarCondition;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "btlCostSec", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int btlCostSec
		{
			get
			{
				return this._btlCostSec;
			}
			set
			{
				this._btlCostSec = value;
			}
		}

		[ProtoMember(12, Name = "items", DataFormat = DataFormat.Default)]
		public List<BountyTaskResultNty.DropItemInfo> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
