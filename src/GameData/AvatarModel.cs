using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "AvatarModel")]
	[Serializable]
	public class AvatarModel : IExtensible
	{
		private int _id;

		private int _listlv;

		private int _icon;

		private int _pic;

		private int _speed;

		private float _RotateSpeed;

		private int _rotateInterval;

		private int _startAngle;

		private int _finishAngle;

		private int _lockDirection;

		private readonly List<int> _diefx = new List<int>();

		private int _flopRange;

		private int _flopAngle;

		private float _scale;

		private float _fxScale;

		private float _undAtkFxScale;

		private readonly List<int> _undAtkFxOffset = new List<int>();

		private string _path = string.Empty;

		private string _action = string.Empty;

		private int _collideOff;

		private int _alwaysVisible;

		private int _fly;

		private int _height_HP;

		private readonly List<int> _bloodBar = new List<int>();

		private int _height_Damage;

		private float _modelRadius;

		private float _modelHeight;

		private float _defaultModelHeight;

		private float _floatProba;

		private float _hitMove;

		private int _hitstraight;

		private readonly List<float> _camRevise = new List<float>();

		private readonly List<float> _camProjRotRevise = new List<float>();

		private float _camProjPosRevise;

		private float _modelProjRotateRevise;

		private float _modelProjYRevise;

		private int _curve;

		private float _frameRatio;

		private int _alertTime;

		private float _projectorScale;

		private int _noTurn;

		private float _CameraDistance;

		private float _CameraHeight;

		private int _mineCar;

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "listlv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int listlv
		{
			get
			{
				return this._listlv;
			}
			set
			{
				this._listlv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "pic", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pic
		{
			get
			{
				return this._pic;
			}
			set
			{
				this._pic = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "speed", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int speed
		{
			get
			{
				return this._speed;
			}
			set
			{
				this._speed = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "RotateSpeed", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float RotateSpeed
		{
			get
			{
				return this._RotateSpeed;
			}
			set
			{
				this._RotateSpeed = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "rotateInterval", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rotateInterval
		{
			get
			{
				return this._rotateInterval;
			}
			set
			{
				this._rotateInterval = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "startAngle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int startAngle
		{
			get
			{
				return this._startAngle;
			}
			set
			{
				this._startAngle = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "finishAngle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int finishAngle
		{
			get
			{
				return this._finishAngle;
			}
			set
			{
				this._finishAngle = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "lockDirection", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lockDirection
		{
			get
			{
				return this._lockDirection;
			}
			set
			{
				this._lockDirection = value;
			}
		}

		[ProtoMember(13, Name = "diefx", DataFormat = DataFormat.TwosComplement)]
		public List<int> diefx
		{
			get
			{
				return this._diefx;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "flopRange", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int flopRange
		{
			get
			{
				return this._flopRange;
			}
			set
			{
				this._flopRange = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "flopAngle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int flopAngle
		{
			get
			{
				return this._flopAngle;
			}
			set
			{
				this._flopAngle = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "scale", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._scale = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "fxScale", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float fxScale
		{
			get
			{
				return this._fxScale;
			}
			set
			{
				this._fxScale = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "undAtkFxScale", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float undAtkFxScale
		{
			get
			{
				return this._undAtkFxScale;
			}
			set
			{
				this._undAtkFxScale = value;
			}
		}

		[ProtoMember(19, Name = "undAtkFxOffset", DataFormat = DataFormat.TwosComplement)]
		public List<int> undAtkFxOffset
		{
			get
			{
				return this._undAtkFxOffset;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "path", DataFormat = DataFormat.Default), DefaultValue("")]
		public string path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "action", DataFormat = DataFormat.Default), DefaultValue("")]
		public string action
		{
			get
			{
				return this._action;
			}
			set
			{
				this._action = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "collideOff", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int collideOff
		{
			get
			{
				return this._collideOff;
			}
			set
			{
				this._collideOff = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "alwaysVisible", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int alwaysVisible
		{
			get
			{
				return this._alwaysVisible;
			}
			set
			{
				this._alwaysVisible = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "fly", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fly
		{
			get
			{
				return this._fly;
			}
			set
			{
				this._fly = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "height_HP", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int height_HP
		{
			get
			{
				return this._height_HP;
			}
			set
			{
				this._height_HP = value;
			}
		}

		[ProtoMember(26, Name = "bloodBar", DataFormat = DataFormat.TwosComplement)]
		public List<int> bloodBar
		{
			get
			{
				return this._bloodBar;
			}
		}

		[ProtoMember(27, IsRequired = false, Name = "height_Damage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int height_Damage
		{
			get
			{
				return this._height_Damage;
			}
			set
			{
				this._height_Damage = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "modelRadius", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float modelRadius
		{
			get
			{
				return this._modelRadius;
			}
			set
			{
				this._modelRadius = value;
			}
		}

		[ProtoMember(29, IsRequired = false, Name = "modelHeight", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float modelHeight
		{
			get
			{
				return this._modelHeight;
			}
			set
			{
				this._modelHeight = value;
			}
		}

		[ProtoMember(30, IsRequired = false, Name = "defaultModelHeight", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float defaultModelHeight
		{
			get
			{
				return this._defaultModelHeight;
			}
			set
			{
				this._defaultModelHeight = value;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "floatProba", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float floatProba
		{
			get
			{
				return this._floatProba;
			}
			set
			{
				this._floatProba = value;
			}
		}

		[ProtoMember(32, IsRequired = false, Name = "hitMove", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float hitMove
		{
			get
			{
				return this._hitMove;
			}
			set
			{
				this._hitMove = value;
			}
		}

		[ProtoMember(33, IsRequired = false, Name = "hitstraight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitstraight
		{
			get
			{
				return this._hitstraight;
			}
			set
			{
				this._hitstraight = value;
			}
		}

		[ProtoMember(34, Name = "camRevise", DataFormat = DataFormat.FixedSize)]
		public List<float> camRevise
		{
			get
			{
				return this._camRevise;
			}
		}

		[ProtoMember(35, Name = "camProjRotRevise", DataFormat = DataFormat.FixedSize)]
		public List<float> camProjRotRevise
		{
			get
			{
				return this._camProjRotRevise;
			}
		}

		[ProtoMember(36, IsRequired = false, Name = "camProjPosRevise", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float camProjPosRevise
		{
			get
			{
				return this._camProjPosRevise;
			}
			set
			{
				this._camProjPosRevise = value;
			}
		}

		[ProtoMember(37, IsRequired = false, Name = "modelProjRotateRevise", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float modelProjRotateRevise
		{
			get
			{
				return this._modelProjRotateRevise;
			}
			set
			{
				this._modelProjRotateRevise = value;
			}
		}

		[ProtoMember(38, IsRequired = false, Name = "modelProjYRevise", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float modelProjYRevise
		{
			get
			{
				return this._modelProjYRevise;
			}
			set
			{
				this._modelProjYRevise = value;
			}
		}

		[ProtoMember(39, IsRequired = false, Name = "curve", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int curve
		{
			get
			{
				return this._curve;
			}
			set
			{
				this._curve = value;
			}
		}

		[ProtoMember(40, IsRequired = false, Name = "frameRatio", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float frameRatio
		{
			get
			{
				return this._frameRatio;
			}
			set
			{
				this._frameRatio = value;
			}
		}

		[ProtoMember(41, IsRequired = false, Name = "alertTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int alertTime
		{
			get
			{
				return this._alertTime;
			}
			set
			{
				this._alertTime = value;
			}
		}

		[ProtoMember(42, IsRequired = false, Name = "projectorScale", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float projectorScale
		{
			get
			{
				return this._projectorScale;
			}
			set
			{
				this._projectorScale = value;
			}
		}

		[ProtoMember(43, IsRequired = false, Name = "noTurn", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int noTurn
		{
			get
			{
				return this._noTurn;
			}
			set
			{
				this._noTurn = value;
			}
		}

		[ProtoMember(44, IsRequired = false, Name = "CameraDistance", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float CameraDistance
		{
			get
			{
				return this._CameraDistance;
			}
			set
			{
				this._CameraDistance = value;
			}
		}

		[ProtoMember(45, IsRequired = false, Name = "CameraHeight", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float CameraHeight
		{
			get
			{
				return this._CameraHeight;
			}
			set
			{
				this._CameraHeight = value;
			}
		}

		[ProtoMember(46, IsRequired = false, Name = "mineCar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mineCar
		{
			get
			{
				return this._mineCar;
			}
			set
			{
				this._mineCar = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
