using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "dailyTask")]
	[Serializable]
	public class dailyTask : IExtensible
	{
		private int _id;

		private string _system1 = string.Empty;

		private string _system2 = string.Empty;

		private int _completeTime;

		private int _iconId;

		private int _vitality;

		private readonly List<int> _refreshTime = new List<int>();

		private int _duration;

		private int _rewardIntroduction;

		private int _reward;

		private int _introduction1;

		private int _introduction2;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "system1", DataFormat = DataFormat.Default), DefaultValue("")]
		public string system1
		{
			get
			{
				return this._system1;
			}
			set
			{
				this._system1 = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "system2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string system2
		{
			get
			{
				return this._system2;
			}
			set
			{
				this._system2 = value;
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

		[ProtoMember(7, Name = "refreshTime", DataFormat = DataFormat.TwosComplement)]
		public List<int> refreshTime
		{
			get
			{
				return this._refreshTime;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "duration", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this._duration = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "rewardIntroduction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardIntroduction
		{
			get
			{
				return this._rewardIntroduction;
			}
			set
			{
				this._rewardIntroduction = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "reward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reward
		{
			get
			{
				return this._reward;
			}
			set
			{
				this._reward = value;
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
