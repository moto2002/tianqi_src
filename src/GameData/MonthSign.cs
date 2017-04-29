using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "MonthSign")]
	[Serializable]
	public class MonthSign : IExtensible
	{
		private int _id;

		private readonly List<int> _date = new List<int>();

		private int _itemId;

		private int _itemNum;

		private int _doubleMinVip;

		private int _resignCost;

		private int _nextID;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, Name = "date", DataFormat = DataFormat.TwosComplement)]
		public List<int> date
		{
			get
			{
				return this._date;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "itemNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "doubleMinVip", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int doubleMinVip
		{
			get
			{
				return this._doubleMinVip;
			}
			set
			{
				this._doubleMinVip = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "resignCost", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resignCost
		{
			get
			{
				return this._resignCost;
			}
			set
			{
				this._resignCost = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "nextID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nextID
		{
			get
			{
				return this._nextID;
			}
			set
			{
				this._nextID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
