using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "VipXianShiQia")]
	[Serializable]
	public class VipXianShiQia : IExtensible
	{
		private int _ID;

		private int _icon;

		private int _name;

		private int _Diamond;

		private int _dayAmount;

		private int _mailId;

		private readonly List<int> _timeLimit = new List<int>();

		private int _buyingTime;

		private int _addExp;

		private int _buyConsult;

		private int _titleConsult;

		private int _titleId;

		private int _addDiamonds;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "icon", DataFormat = DataFormat.TwosComplement)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "name", DataFormat = DataFormat.TwosComplement)]
		public int name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "Diamond", DataFormat = DataFormat.TwosComplement)]
		public int Diamond
		{
			get
			{
				return this._Diamond;
			}
			set
			{
				this._Diamond = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "dayAmount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dayAmount
		{
			get
			{
				return this._dayAmount;
			}
			set
			{
				this._dayAmount = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "mailId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mailId
		{
			get
			{
				return this._mailId;
			}
			set
			{
				this._mailId = value;
			}
		}

		[ProtoMember(8, Name = "timeLimit", DataFormat = DataFormat.TwosComplement)]
		public List<int> timeLimit
		{
			get
			{
				return this._timeLimit;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "buyingTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buyingTime
		{
			get
			{
				return this._buyingTime;
			}
			set
			{
				this._buyingTime = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "addExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int addExp
		{
			get
			{
				return this._addExp;
			}
			set
			{
				this._addExp = value;
			}
		}

		[ProtoMember(11, IsRequired = true, Name = "buyConsult", DataFormat = DataFormat.TwosComplement)]
		public int buyConsult
		{
			get
			{
				return this._buyConsult;
			}
			set
			{
				this._buyConsult = value;
			}
		}

		[ProtoMember(12, IsRequired = true, Name = "titleConsult", DataFormat = DataFormat.TwosComplement)]
		public int titleConsult
		{
			get
			{
				return this._titleConsult;
			}
			set
			{
				this._titleConsult = value;
			}
		}

		[ProtoMember(13, IsRequired = true, Name = "titleId", DataFormat = DataFormat.TwosComplement)]
		public int titleId
		{
			get
			{
				return this._titleId;
			}
			set
			{
				this._titleId = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "addDiamonds", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int addDiamonds
		{
			get
			{
				return this._addDiamonds;
			}
			set
			{
				this._addDiamonds = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
