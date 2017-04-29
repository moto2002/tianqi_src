using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1712), ForSend(1712), ProtoContract(Name = "FundListPush")]
	[Serializable]
	public class FundListPush : IExtensible
	{
		[ProtoContract(Name = "Items")]
		[Serializable]
		public class Items : IExtensible
		{
			private int _days;

			private int _itemId;

			private int _itemNum;

			private bool _canGetFlag;

			private bool _hasGetPrize;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = false, Name = "days", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int days
			{
				get
				{
					return this._days;
				}
				set
				{
					this._days = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

			[ProtoMember(3, IsRequired = false, Name = "itemNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int itemNum
			{
				get
				{
					return this._itemNum;
				}
				set
				{
					this._itemNum = value;
				}
			}

			[ProtoMember(4, IsRequired = false, Name = "canGetFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
			public bool canGetFlag
			{
				get
				{
					return this._canGetFlag;
				}
				set
				{
					this._canGetFlag = value;
				}
			}

			[ProtoMember(5, IsRequired = false, Name = "hasGetPrize", DataFormat = DataFormat.Default), DefaultValue(false)]
			public bool hasGetPrize
			{
				get
				{
					return this._hasGetPrize;
				}
				set
				{
					this._hasGetPrize = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		[ProtoContract(Name = "Times")]
		[Serializable]
		public class Times : IExtensible
		{
			private int _beginTime;

			private int _endTime;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = false, Name = "beginTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int beginTime
			{
				get
				{
					return this._beginTime;
				}
				set
				{
					this._beginTime = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "endTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int endTime
			{
				get
				{
					return this._endTime;
				}
				set
				{
					this._endTime = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 1712;

		private int _typeId;

		private bool _hasBuy;

		private int _price;

		private readonly List<FundListPush.Items> _item = new List<FundListPush.Items>();

		private readonly List<FundListPush.Times> _Time = new List<FundListPush.Times>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "hasBuy", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hasBuy
		{
			get
			{
				return this._hasBuy;
			}
			set
			{
				this._hasBuy = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "price", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, Name = "item", DataFormat = DataFormat.Default)]
		public List<FundListPush.Items> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(5, Name = "Time", DataFormat = DataFormat.Default)]
		public List<FundListPush.Times> Time
		{
			get
			{
				return this._Time;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
