using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "HuoDongZhongXin")]
	[Serializable]
	public class HuoDongZhongXin : IExtensible
	{
		private int _activityid;

		private string _activities = string.Empty;

		private int _team;

		private int _people;

		private int _minLv;

		private int _num;

		private int _preparetime;

		private readonly List<string> _starttime = new List<string>();

		private readonly List<string> _endtime = new List<string>();

		private readonly List<int> _date = new List<int>();

		private readonly List<int> _award = new List<int>();

		private string _picture = string.Empty;

		private readonly List<int> _stroke = new List<int>();

		private int _delaytime;

		private int _Icon;

		private int _show;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "activityid", DataFormat = DataFormat.TwosComplement)]
		public int activityid
		{
			get
			{
				return this._activityid;
			}
			set
			{
				this._activityid = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "activities", DataFormat = DataFormat.Default), DefaultValue("")]
		public string activities
		{
			get
			{
				return this._activities;
			}
			set
			{
				this._activities = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "team", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int team
		{
			get
			{
				return this._team;
			}
			set
			{
				this._team = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "people", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int people
		{
			get
			{
				return this._people;
			}
			set
			{
				this._people = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "preparetime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int preparetime
		{
			get
			{
				return this._preparetime;
			}
			set
			{
				this._preparetime = value;
			}
		}

		[ProtoMember(9, Name = "starttime", DataFormat = DataFormat.Default)]
		public List<string> starttime
		{
			get
			{
				return this._starttime;
			}
		}

		[ProtoMember(10, Name = "endtime", DataFormat = DataFormat.Default)]
		public List<string> endtime
		{
			get
			{
				return this._endtime;
			}
		}

		[ProtoMember(11, Name = "date", DataFormat = DataFormat.TwosComplement)]
		public List<int> date
		{
			get
			{
				return this._date;
			}
		}

		[ProtoMember(12, Name = "award", DataFormat = DataFormat.TwosComplement)]
		public List<int> award
		{
			get
			{
				return this._award;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "picture", DataFormat = DataFormat.Default), DefaultValue("")]
		public string picture
		{
			get
			{
				return this._picture;
			}
			set
			{
				this._picture = value;
			}
		}

		[ProtoMember(14, Name = "stroke", DataFormat = DataFormat.TwosComplement)]
		public List<int> stroke
		{
			get
			{
				return this._stroke;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "delaytime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int delaytime
		{
			get
			{
				return this._delaytime;
			}
			set
			{
				this._delaytime = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "Icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Icon
		{
			get
			{
				return this._Icon;
			}
			set
			{
				this._Icon = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "show", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int show
		{
			get
			{
				return this._show;
			}
			set
			{
				this._show = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
