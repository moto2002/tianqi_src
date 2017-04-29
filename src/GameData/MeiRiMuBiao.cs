using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "MeiRiMuBiao")]
	[Serializable]
	public class MeiRiMuBiao : IExtensible
	{
		private int _id;

		private int _system;

		private int _completeTime;

		private int _iconId;

		private int _vitality;

		private int _openLv;

		private int _sysId;

		private string _refreshTime = string.Empty;

		private readonly List<int> _rewardIntroductionIcon = new List<int>();

		private int _priority;

		private int _introduction1;

		private int _introduction2;

		private int _introduction3;

		private int _introduction4;

		private int _Retrieve;

		private int _buyTime;

		private int _activity;

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

		[ProtoMember(4, IsRequired = false, Name = "completeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int completeTime
		{
			get
			{
				return this._completeTime;
			}
			set
			{
				this._completeTime = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "iconId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int iconId
		{
			get
			{
				return this._iconId;
			}
			set
			{
				this._iconId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "vitality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vitality
		{
			get
			{
				return this._vitality;
			}
			set
			{
				this._vitality = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "openLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "sysId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sysId
		{
			get
			{
				return this._sysId;
			}
			set
			{
				this._sysId = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "refreshTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string refreshTime
		{
			get
			{
				return this._refreshTime;
			}
			set
			{
				this._refreshTime = value;
			}
		}

		[ProtoMember(10, Name = "rewardIntroductionIcon", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardIntroductionIcon
		{
			get
			{
				return this._rewardIntroductionIcon;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "priority", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				this._priority = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "introduction1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int introduction1
		{
			get
			{
				return this._introduction1;
			}
			set
			{
				this._introduction1 = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "introduction2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int introduction2
		{
			get
			{
				return this._introduction2;
			}
			set
			{
				this._introduction2 = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "introduction3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int introduction3
		{
			get
			{
				return this._introduction3;
			}
			set
			{
				this._introduction3 = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "introduction4", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int introduction4
		{
			get
			{
				return this._introduction4;
			}
			set
			{
				this._introduction4 = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "Retrieve", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Retrieve
		{
			get
			{
				return this._Retrieve;
			}
			set
			{
				this._Retrieve = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "buyTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buyTime
		{
			get
			{
				return this._buyTime;
			}
			set
			{
				this._buyTime = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "activity", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activity
		{
			get
			{
				return this._activity;
			}
			set
			{
				this._activity = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
