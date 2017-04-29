using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1713), ForSend(1713), ProtoContract(Name = "GrowthPlanListPush")]
	[Serializable]
	public class GrowthPlanListPush : IExtensible
	{
		[ProtoContract(Name = "Items")]
		[Serializable]
		public class Items : IExtensible
		{
			private int _roleLv;

			private int _itemId;

			private int _itemNum;

			private bool _canGetFlag;

			private bool _hasGetPrize;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = false, Name = "roleLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int roleLv
			{
				get
				{
					return this._roleLv;
				}
				set
				{
					this._roleLv = value;
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

		public static readonly short OP = 1713;

		private int _typeId;

		private bool _hasBuy;

		private int _price;

		private readonly List<GrowthPlanListPush.Items> _item = new List<GrowthPlanListPush.Items>();

		private int _vipLv;

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
		public List<GrowthPlanListPush.Items> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "vipLv", DataFormat = DataFormat.TwosComplement)]
		public int vipLv
		{
			get
			{
				return this._vipLv;
			}
			set
			{
				this._vipLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
