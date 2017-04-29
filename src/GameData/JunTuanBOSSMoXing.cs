using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JunTuanBOSSMoXing")]
	[Serializable]
	public class JunTuanBOSSMoXing : IExtensible
	{
		private int _bossId;

		private int _bossLv;

		private readonly List<float> _modelOffset = new List<float>();

		private float _modelAngle;

		private float _modelZoom;

		private int _fuFen;

		private int _rank;

		private string _picture = string.Empty;

		private int _HurtReward;

		private int _KillReward;

		private string _OneReward = string.Empty;

		private string _TwoReward = string.Empty;

		private string _ThreeReward = string.Empty;

		private string _LastReward = string.Empty;

		private string _placingReward = string.Empty;

		private int _CorpsReward;

		private int _rewardShow;

		private readonly List<int> _reward = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "bossId", DataFormat = DataFormat.TwosComplement)]
		public int bossId
		{
			get
			{
				return this._bossId;
			}
			set
			{
				this._bossId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "bossLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossLv
		{
			get
			{
				return this._bossLv;
			}
			set
			{
				this._bossLv = value;
			}
		}

		[ProtoMember(4, Name = "modelOffset", DataFormat = DataFormat.FixedSize)]
		public List<float> modelOffset
		{
			get
			{
				return this._modelOffset;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "modelAngle", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float modelAngle
		{
			get
			{
				return this._modelAngle;
			}
			set
			{
				this._modelAngle = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "modelZoom", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float modelZoom
		{
			get
			{
				return this._modelZoom;
			}
			set
			{
				this._modelZoom = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "fuFen", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fuFen
		{
			get
			{
				return this._fuFen;
			}
			set
			{
				this._fuFen = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "rank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				this._rank = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "picture", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(11, IsRequired = false, Name = "HurtReward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int HurtReward
		{
			get
			{
				return this._HurtReward;
			}
			set
			{
				this._HurtReward = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "KillReward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int KillReward
		{
			get
			{
				return this._KillReward;
			}
			set
			{
				this._KillReward = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "OneReward", DataFormat = DataFormat.Default), DefaultValue("")]
		public string OneReward
		{
			get
			{
				return this._OneReward;
			}
			set
			{
				this._OneReward = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "TwoReward", DataFormat = DataFormat.Default), DefaultValue("")]
		public string TwoReward
		{
			get
			{
				return this._TwoReward;
			}
			set
			{
				this._TwoReward = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "ThreeReward", DataFormat = DataFormat.Default), DefaultValue("")]
		public string ThreeReward
		{
			get
			{
				return this._ThreeReward;
			}
			set
			{
				this._ThreeReward = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "LastReward", DataFormat = DataFormat.Default), DefaultValue("")]
		public string LastReward
		{
			get
			{
				return this._LastReward;
			}
			set
			{
				this._LastReward = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "placingReward", DataFormat = DataFormat.Default), DefaultValue("")]
		public string placingReward
		{
			get
			{
				return this._placingReward;
			}
			set
			{
				this._placingReward = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "CorpsReward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int CorpsReward
		{
			get
			{
				return this._CorpsReward;
			}
			set
			{
				this._CorpsReward = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "rewardShow", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardShow
		{
			get
			{
				return this._rewardShow;
			}
			set
			{
				this._rewardShow = value;
			}
		}

		[ProtoMember(20, Name = "reward", DataFormat = DataFormat.TwosComplement)]
		public List<int> reward
		{
			get
			{
				return this._reward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
