using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Effect")]
	[Serializable]
	public class Effect : IExtensible
	{
		[ProtoContract(Name = "RolepropidPair")]
		[Serializable]
		public class RolepropidPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		[ProtoContract(Name = "GradeaddloopbuffidPair")]
		[Serializable]
		public class GradeaddloopbuffidPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		[ProtoContract(Name = "GradeaddbuffidPair")]
		[Serializable]
		public class GradeaddbuffidPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _id;

		private readonly List<int> _damageType = new List<int>();

		private int _probability;

		private int _element;

		private int _targetType;

		private int _antiaircraft;

		private int _type1;

		private int _aiEffectMove;

		private int _type2;

		private int _bullet;

		private int _collisionTimes;

		private readonly List<int> _bulletOffset = new List<int>();

		private int _base;

		private readonly List<int> _range = new List<int>();

		private readonly List<int> _range2 = new List<int>();

		private readonly List<int> _offset = new List<int>();

		private readonly List<int> _offset2 = new List<int>();

		private int _tremble;

		private int _forwardFixAngle;

		private readonly List<int> _coord = new List<int>();

		private readonly List<int> _orientation = new List<int>();

		private int _quantity;

		private int _summonId;

		private int _blinkPoint;

		private int _monsterId;

		private int _audio;

		private int _fx;

		private int _fxRepeated;

		private int _delay;

		private int _interval;

		private int _time;

		private int _hitAudio;

		private int _hitFx;

		private string _hitAction = string.Empty;

		private int _hitActionPriority;

		private readonly List<float> _hitMove = new List<float>();

		private int _hitMoveEffect;

		private int _hitstraight;

		private int _hitBase;

		private float _frameFroze;

		private int _frameTime;

		private int _frameInterval;

		private int _casterPoint;

		private int _targetPoint;

		private readonly List<int> _addLoopBuff = new List<int>();

		private readonly List<int> _addBuff = new List<int>();

		private readonly List<int> _removeBuff = new List<int>();

		private int _forcePickup;

		private readonly List<int> _cameraEffect = new List<int>();

		private int _cameraRelyPickup;

		private int _teleport;

		private int _propId;

		private readonly List<Effect.RolepropidPair> _rolePropId = new List<Effect.RolepropidPair>();

		private int _bulletRange;

		private int _bulletCameraEffect;

		private int _bulletFx;

		private int _cycleHit;

		private readonly List<Effect.GradeaddloopbuffidPair> _gradeAddLoopBuffId = new List<Effect.GradeaddloopbuffidPair>();

		private readonly List<Effect.GradeaddbuffidPair> _gradeAddBuffId = new List<Effect.GradeaddbuffidPair>();

		private int _allowHit;

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

		[ProtoMember(4, Name = "damageType", DataFormat = DataFormat.TwosComplement)]
		public List<int> damageType
		{
			get
			{
				return this._damageType;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "element", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int element
		{
			get
			{
				return this._element;
			}
			set
			{
				this._element = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "targetType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetType
		{
			get
			{
				return this._targetType;
			}
			set
			{
				this._targetType = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "antiaircraft", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int antiaircraft
		{
			get
			{
				return this._antiaircraft;
			}
			set
			{
				this._antiaircraft = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "type1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type1
		{
			get
			{
				return this._type1;
			}
			set
			{
				this._type1 = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "aiEffectMove", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int aiEffectMove
		{
			get
			{
				return this._aiEffectMove;
			}
			set
			{
				this._aiEffectMove = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "type2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type2
		{
			get
			{
				return this._type2;
			}
			set
			{
				this._type2 = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "bullet", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bullet
		{
			get
			{
				return this._bullet;
			}
			set
			{
				this._bullet = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "collisionTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int collisionTimes
		{
			get
			{
				return this._collisionTimes;
			}
			set
			{
				this._collisionTimes = value;
			}
		}

		[ProtoMember(14, Name = "bulletOffset", DataFormat = DataFormat.TwosComplement)]
		public List<int> bulletOffset
		{
			get
			{
				return this._bulletOffset;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "base", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int @base
		{
			get
			{
				return this._base;
			}
			set
			{
				this._base = value;
			}
		}

		[ProtoMember(16, Name = "range", DataFormat = DataFormat.TwosComplement)]
		public List<int> range
		{
			get
			{
				return this._range;
			}
		}

		[ProtoMember(17, Name = "range2", DataFormat = DataFormat.TwosComplement)]
		public List<int> range2
		{
			get
			{
				return this._range2;
			}
		}

		[ProtoMember(18, Name = "offset", DataFormat = DataFormat.TwosComplement)]
		public List<int> offset
		{
			get
			{
				return this._offset;
			}
		}

		[ProtoMember(19, Name = "offset2", DataFormat = DataFormat.TwosComplement)]
		public List<int> offset2
		{
			get
			{
				return this._offset2;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "tremble", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tremble
		{
			get
			{
				return this._tremble;
			}
			set
			{
				this._tremble = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "forwardFixAngle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int forwardFixAngle
		{
			get
			{
				return this._forwardFixAngle;
			}
			set
			{
				this._forwardFixAngle = value;
			}
		}

		[ProtoMember(22, Name = "coord", DataFormat = DataFormat.TwosComplement)]
		public List<int> coord
		{
			get
			{
				return this._coord;
			}
		}

		[ProtoMember(23, Name = "orientation", DataFormat = DataFormat.TwosComplement)]
		public List<int> orientation
		{
			get
			{
				return this._orientation;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "quantity", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int quantity
		{
			get
			{
				return this._quantity;
			}
			set
			{
				this._quantity = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "summonId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int summonId
		{
			get
			{
				return this._summonId;
			}
			set
			{
				this._summonId = value;
			}
		}

		[ProtoMember(26, IsRequired = false, Name = "blinkPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int blinkPoint
		{
			get
			{
				return this._blinkPoint;
			}
			set
			{
				this._blinkPoint = value;
			}
		}

		[ProtoMember(27, IsRequired = false, Name = "monsterId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterId
		{
			get
			{
				return this._monsterId;
			}
			set
			{
				this._monsterId = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "audio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int audio
		{
			get
			{
				return this._audio;
			}
			set
			{
				this._audio = value;
			}
		}

		[ProtoMember(29, IsRequired = false, Name = "fx", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fx
		{
			get
			{
				return this._fx;
			}
			set
			{
				this._fx = value;
			}
		}

		[ProtoMember(30, IsRequired = false, Name = "fxRepeated", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fxRepeated
		{
			get
			{
				return this._fxRepeated;
			}
			set
			{
				this._fxRepeated = value;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "delay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int delay
		{
			get
			{
				return this._delay;
			}
			set
			{
				this._delay = value;
			}
		}

		[ProtoMember(32, IsRequired = false, Name = "interval", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int interval
		{
			get
			{
				return this._interval;
			}
			set
			{
				this._interval = value;
			}
		}

		[ProtoMember(33, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(34, IsRequired = false, Name = "hitAudio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitAudio
		{
			get
			{
				return this._hitAudio;
			}
			set
			{
				this._hitAudio = value;
			}
		}

		[ProtoMember(35, IsRequired = false, Name = "hitFx", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitFx
		{
			get
			{
				return this._hitFx;
			}
			set
			{
				this._hitFx = value;
			}
		}

		[ProtoMember(36, IsRequired = false, Name = "hitAction", DataFormat = DataFormat.Default), DefaultValue("")]
		public string hitAction
		{
			get
			{
				return this._hitAction;
			}
			set
			{
				this._hitAction = value;
			}
		}

		[ProtoMember(37, IsRequired = false, Name = "hitActionPriority", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitActionPriority
		{
			get
			{
				return this._hitActionPriority;
			}
			set
			{
				this._hitActionPriority = value;
			}
		}

		[ProtoMember(38, Name = "hitMove", DataFormat = DataFormat.FixedSize)]
		public List<float> hitMove
		{
			get
			{
				return this._hitMove;
			}
		}

		[ProtoMember(39, IsRequired = false, Name = "hitMoveEffect", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitMoveEffect
		{
			get
			{
				return this._hitMoveEffect;
			}
			set
			{
				this._hitMoveEffect = value;
			}
		}

		[ProtoMember(40, IsRequired = false, Name = "hitstraight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(41, IsRequired = false, Name = "hitBase", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitBase
		{
			get
			{
				return this._hitBase;
			}
			set
			{
				this._hitBase = value;
			}
		}

		[ProtoMember(42, IsRequired = false, Name = "frameFroze", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float frameFroze
		{
			get
			{
				return this._frameFroze;
			}
			set
			{
				this._frameFroze = value;
			}
		}

		[ProtoMember(43, IsRequired = false, Name = "frameTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int frameTime
		{
			get
			{
				return this._frameTime;
			}
			set
			{
				this._frameTime = value;
			}
		}

		[ProtoMember(44, IsRequired = false, Name = "frameInterval", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int frameInterval
		{
			get
			{
				return this._frameInterval;
			}
			set
			{
				this._frameInterval = value;
			}
		}

		[ProtoMember(45, IsRequired = false, Name = "casterPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int casterPoint
		{
			get
			{
				return this._casterPoint;
			}
			set
			{
				this._casterPoint = value;
			}
		}

		[ProtoMember(46, IsRequired = false, Name = "targetPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetPoint
		{
			get
			{
				return this._targetPoint;
			}
			set
			{
				this._targetPoint = value;
			}
		}

		[ProtoMember(47, Name = "addLoopBuff", DataFormat = DataFormat.TwosComplement)]
		public List<int> addLoopBuff
		{
			get
			{
				return this._addLoopBuff;
			}
		}

		[ProtoMember(48, Name = "addBuff", DataFormat = DataFormat.TwosComplement)]
		public List<int> addBuff
		{
			get
			{
				return this._addBuff;
			}
		}

		[ProtoMember(49, Name = "removeBuff", DataFormat = DataFormat.TwosComplement)]
		public List<int> removeBuff
		{
			get
			{
				return this._removeBuff;
			}
		}

		[ProtoMember(50, IsRequired = false, Name = "forcePickup", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int forcePickup
		{
			get
			{
				return this._forcePickup;
			}
			set
			{
				this._forcePickup = value;
			}
		}

		[ProtoMember(51, Name = "cameraEffect", DataFormat = DataFormat.TwosComplement)]
		public List<int> cameraEffect
		{
			get
			{
				return this._cameraEffect;
			}
		}

		[ProtoMember(52, IsRequired = false, Name = "cameraRelyPickup", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cameraRelyPickup
		{
			get
			{
				return this._cameraRelyPickup;
			}
			set
			{
				this._cameraRelyPickup = value;
			}
		}

		[ProtoMember(53, IsRequired = false, Name = "teleport", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int teleport
		{
			get
			{
				return this._teleport;
			}
			set
			{
				this._teleport = value;
			}
		}

		[ProtoMember(54, IsRequired = false, Name = "propId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int propId
		{
			get
			{
				return this._propId;
			}
			set
			{
				this._propId = value;
			}
		}

		[ProtoMember(55, Name = "rolePropId", DataFormat = DataFormat.Default)]
		public List<Effect.RolepropidPair> rolePropId
		{
			get
			{
				return this._rolePropId;
			}
		}

		[ProtoMember(56, IsRequired = false, Name = "bulletRange", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bulletRange
		{
			get
			{
				return this._bulletRange;
			}
			set
			{
				this._bulletRange = value;
			}
		}

		[ProtoMember(57, IsRequired = false, Name = "bulletCameraEffect", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bulletCameraEffect
		{
			get
			{
				return this._bulletCameraEffect;
			}
			set
			{
				this._bulletCameraEffect = value;
			}
		}

		[ProtoMember(58, IsRequired = false, Name = "bulletFx", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bulletFx
		{
			get
			{
				return this._bulletFx;
			}
			set
			{
				this._bulletFx = value;
			}
		}

		[ProtoMember(59, IsRequired = false, Name = "cycleHit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cycleHit
		{
			get
			{
				return this._cycleHit;
			}
			set
			{
				this._cycleHit = value;
			}
		}

		[ProtoMember(60, Name = "gradeAddLoopBuffId", DataFormat = DataFormat.Default)]
		public List<Effect.GradeaddloopbuffidPair> gradeAddLoopBuffId
		{
			get
			{
				return this._gradeAddLoopBuffId;
			}
		}

		[ProtoMember(61, Name = "gradeAddBuffId", DataFormat = DataFormat.Default)]
		public List<Effect.GradeaddbuffidPair> gradeAddBuffId
		{
			get
			{
				return this._gradeAddBuffId;
			}
		}

		[ProtoMember(62, IsRequired = false, Name = "allowHit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int allowHit
		{
			get
			{
				return this._allowHit;
			}
			set
			{
				this._allowHit = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
