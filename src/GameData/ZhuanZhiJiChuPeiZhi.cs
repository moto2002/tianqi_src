using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhuanZhiJiChuPeiZhi")]
	[Serializable]
	public class ZhuanZhiJiChuPeiZhi : IExtensible
	{
		private int _job;

		private int _roleCreate;

		private readonly List<int> _jobList = new List<int>();

		private int _maxLevel;

		private int _vipLevel;

		private int _price;

		private readonly List<int> _mission = new List<int>();

		private int _jobPic;

		private int _jobPic1;

		private string _jobPic2 = string.Empty;

		private string _jobPic3 = string.Empty;

		private string _jobPic4 = string.Empty;

		private string _jobPic5 = string.Empty;

		private string _jobNameImage = string.Empty;

		private string _jobNameBg = string.Empty;

		private int _jobName;

		private int _jobDescribe;

		private readonly List<int> _jobEvaluate = new List<int>();

		private int _jobBattle;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "job", DataFormat = DataFormat.TwosComplement)]
		public int job
		{
			get
			{
				return this._job;
			}
			set
			{
				this._job = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "roleCreate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int roleCreate
		{
			get
			{
				return this._roleCreate;
			}
			set
			{
				this._roleCreate = value;
			}
		}

		[ProtoMember(4, Name = "jobList", DataFormat = DataFormat.TwosComplement)]
		public List<int> jobList
		{
			get
			{
				return this._jobList;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "maxLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxLevel
		{
			get
			{
				return this._maxLevel;
			}
			set
			{
				this._maxLevel = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "vipLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vipLevel
		{
			get
			{
				return this._vipLevel;
			}
			set
			{
				this._vipLevel = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "price", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, Name = "mission", DataFormat = DataFormat.TwosComplement)]
		public List<int> mission
		{
			get
			{
				return this._mission;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "jobPic", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int jobPic
		{
			get
			{
				return this._jobPic;
			}
			set
			{
				this._jobPic = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "jobPic1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int jobPic1
		{
			get
			{
				return this._jobPic1;
			}
			set
			{
				this._jobPic1 = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "jobPic2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string jobPic2
		{
			get
			{
				return this._jobPic2;
			}
			set
			{
				this._jobPic2 = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "jobPic3", DataFormat = DataFormat.Default), DefaultValue("")]
		public string jobPic3
		{
			get
			{
				return this._jobPic3;
			}
			set
			{
				this._jobPic3 = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "jobPic4", DataFormat = DataFormat.Default), DefaultValue("")]
		public string jobPic4
		{
			get
			{
				return this._jobPic4;
			}
			set
			{
				this._jobPic4 = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "jobPic5", DataFormat = DataFormat.Default), DefaultValue("")]
		public string jobPic5
		{
			get
			{
				return this._jobPic5;
			}
			set
			{
				this._jobPic5 = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "jobNameImage", DataFormat = DataFormat.Default), DefaultValue("")]
		public string jobNameImage
		{
			get
			{
				return this._jobNameImage;
			}
			set
			{
				this._jobNameImage = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "jobNameBg", DataFormat = DataFormat.Default), DefaultValue("")]
		public string jobNameBg
		{
			get
			{
				return this._jobNameBg;
			}
			set
			{
				this._jobNameBg = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "jobName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int jobName
		{
			get
			{
				return this._jobName;
			}
			set
			{
				this._jobName = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "jobDescribe", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int jobDescribe
		{
			get
			{
				return this._jobDescribe;
			}
			set
			{
				this._jobDescribe = value;
			}
		}

		[ProtoMember(19, Name = "jobEvaluate", DataFormat = DataFormat.TwosComplement)]
		public List<int> jobEvaluate
		{
			get
			{
				return this._jobEvaluate;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "jobBattle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int jobBattle
		{
			get
			{
				return this._jobBattle;
			}
			set
			{
				this._jobBattle = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
