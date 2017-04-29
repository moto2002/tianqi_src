using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Monster")]
	[Serializable]
	public class Monster : IExtensible
	{
		private uint _id;

		private int _monstertype;

		private int _flag;

		private int _buffflag;

		private readonly List<int> _golds = new List<int>();

		private int _model;

		private int _name;

		private int _icon2;

		private int _mainId;

		private int _partId;

		private int _mass;

		private int _actionPoint;

		private float _pointRestore;

		private readonly List<int> _skill = new List<int>();

		private readonly List<string> _aiId = new List<string>();

		private int _aiDelay;

		private float _skillInterval;

		private readonly List<int> _interval = new List<int>();

		private int _aiStop;

		private int _golem;

		private int _disappear;

		private int _noCollide;

		private int _temp;

		private readonly List<float> _colour = new List<float>();

		private int _AttributeTemplateID;

		private int _propId;

		private int _hpAdd;

		private int _attAdd;

		private int _penetrationAdd;

		private int _defAdd;

		private int _vigourAdd;

		private int _hitAdd;

		private int _dexAdd;

		private int _crtAdd;

		private int _parryAdd;

		private int _soulAdd;

		private int _npcMark;

		private int _hitTips;

		private int _dieTips;

		private int _shadow;

		private int _flashWhite;

		private int _floatBlood;

		private int _BloodBar;

		private int _talk;

		private int _arrow;

		private int _invisible;

		private int _nomoreVisible;

		private int _range;

		private int _damageCollect;

		private int _birthAction;

		private int _cameraLock;

		private string _namePic = string.Empty;

		private int _monsterBornDirection;

		private int _scenePoint;

		private int _exclusiveBGM;

		private int _mode;

		private readonly List<int> _triggerCondition = new List<int>();

		private readonly List<int> _triggerSkillId = new List<int>();

		private int _prompt;

		private int _HpAmplificationRate;

		private int _AttAmplificationRate;

		private int _DefAmplificationRate;

		private int _HitAmplificationRate;

		private int _DexAmplificationRate;

		private int _CrtAmplificationRate;

		private int _PenAmplificationRate;

		private int _CthAmplificationRate;

		private int _ParAmplificationRate;

		private int _VigAmplificationRate;

		private int _PrhAmplificationRate;

		private int _AtsAmplificationRate;

		private int _VpAmplificationRate;

		private int _IvAmplificationRate;

		private int _VsAmplificationRate;

		private int _actionPriority;

		private IExtension extensionObject;

		[ProtoMember(6, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public uint id
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

		[ProtoMember(7, IsRequired = false, Name = "monstertype", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monstertype
		{
			get
			{
				return this._monstertype;
			}
			set
			{
				this._monstertype = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "flag", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int flag
		{
			get
			{
				return this._flag;
			}
			set
			{
				this._flag = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "buffflag", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buffflag
		{
			get
			{
				return this._buffflag;
			}
			set
			{
				this._buffflag = value;
			}
		}

		[ProtoMember(10, Name = "golds", DataFormat = DataFormat.TwosComplement)]
		public List<int> golds
		{
			get
			{
				return this._golds;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int model
		{
			get
			{
				return this._model;
			}
			set
			{
				this._model = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "icon2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon2
		{
			get
			{
				return this._icon2;
			}
			set
			{
				this._icon2 = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "mainId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mainId
		{
			get
			{
				return this._mainId;
			}
			set
			{
				this._mainId = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "partId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int partId
		{
			get
			{
				return this._partId;
			}
			set
			{
				this._partId = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "mass", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mass
		{
			get
			{
				return this._mass;
			}
			set
			{
				this._mass = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "actionPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int actionPoint
		{
			get
			{
				return this._actionPoint;
			}
			set
			{
				this._actionPoint = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "pointRestore", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float pointRestore
		{
			get
			{
				return this._pointRestore;
			}
			set
			{
				this._pointRestore = value;
			}
		}

		[ProtoMember(19, Name = "skill", DataFormat = DataFormat.TwosComplement)]
		public List<int> skill
		{
			get
			{
				return this._skill;
			}
		}

		[ProtoMember(20, Name = "aiId", DataFormat = DataFormat.Default)]
		public List<string> aiId
		{
			get
			{
				return this._aiId;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "aiDelay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int aiDelay
		{
			get
			{
				return this._aiDelay;
			}
			set
			{
				this._aiDelay = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "skillInterval", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float skillInterval
		{
			get
			{
				return this._skillInterval;
			}
			set
			{
				this._skillInterval = value;
			}
		}

		[ProtoMember(23, Name = "interval", DataFormat = DataFormat.TwosComplement)]
		public List<int> interval
		{
			get
			{
				return this._interval;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "aiStop", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int aiStop
		{
			get
			{
				return this._aiStop;
			}
			set
			{
				this._aiStop = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "golem", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int golem
		{
			get
			{
				return this._golem;
			}
			set
			{
				this._golem = value;
			}
		}

		[ProtoMember(26, IsRequired = false, Name = "disappear", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int disappear
		{
			get
			{
				return this._disappear;
			}
			set
			{
				this._disappear = value;
			}
		}

		[ProtoMember(27, IsRequired = false, Name = "noCollide", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int noCollide
		{
			get
			{
				return this._noCollide;
			}
			set
			{
				this._noCollide = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "temp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int temp
		{
			get
			{
				return this._temp;
			}
			set
			{
				this._temp = value;
			}
		}

		[ProtoMember(29, Name = "colour", DataFormat = DataFormat.FixedSize)]
		public List<float> colour
		{
			get
			{
				return this._colour;
			}
		}

		[ProtoMember(30, IsRequired = false, Name = "AttributeTemplateID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int AttributeTemplateID
		{
			get
			{
				return this._AttributeTemplateID;
			}
			set
			{
				this._AttributeTemplateID = value;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "propId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(32, IsRequired = false, Name = "hpAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hpAdd
		{
			get
			{
				return this._hpAdd;
			}
			set
			{
				this._hpAdd = value;
			}
		}

		[ProtoMember(33, IsRequired = false, Name = "attAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attAdd
		{
			get
			{
				return this._attAdd;
			}
			set
			{
				this._attAdd = value;
			}
		}

		[ProtoMember(34, IsRequired = false, Name = "penetrationAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int penetrationAdd
		{
			get
			{
				return this._penetrationAdd;
			}
			set
			{
				this._penetrationAdd = value;
			}
		}

		[ProtoMember(35, IsRequired = false, Name = "defAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int defAdd
		{
			get
			{
				return this._defAdd;
			}
			set
			{
				this._defAdd = value;
			}
		}

		[ProtoMember(36, IsRequired = false, Name = "vigourAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vigourAdd
		{
			get
			{
				return this._vigourAdd;
			}
			set
			{
				this._vigourAdd = value;
			}
		}

		[ProtoMember(37, IsRequired = false, Name = "hitAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitAdd
		{
			get
			{
				return this._hitAdd;
			}
			set
			{
				this._hitAdd = value;
			}
		}

		[ProtoMember(38, IsRequired = false, Name = "dexAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dexAdd
		{
			get
			{
				return this._dexAdd;
			}
			set
			{
				this._dexAdd = value;
			}
		}

		[ProtoMember(39, IsRequired = false, Name = "crtAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int crtAdd
		{
			get
			{
				return this._crtAdd;
			}
			set
			{
				this._crtAdd = value;
			}
		}

		[ProtoMember(40, IsRequired = false, Name = "parryAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int parryAdd
		{
			get
			{
				return this._parryAdd;
			}
			set
			{
				this._parryAdd = value;
			}
		}

		[ProtoMember(41, IsRequired = false, Name = "soulAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int soulAdd
		{
			get
			{
				return this._soulAdd;
			}
			set
			{
				this._soulAdd = value;
			}
		}

		[ProtoMember(42, IsRequired = false, Name = "npcMark", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int npcMark
		{
			get
			{
				return this._npcMark;
			}
			set
			{
				this._npcMark = value;
			}
		}

		[ProtoMember(43, IsRequired = false, Name = "hitTips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitTips
		{
			get
			{
				return this._hitTips;
			}
			set
			{
				this._hitTips = value;
			}
		}

		[ProtoMember(44, IsRequired = false, Name = "dieTips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dieTips
		{
			get
			{
				return this._dieTips;
			}
			set
			{
				this._dieTips = value;
			}
		}

		[ProtoMember(45, IsRequired = false, Name = "shadow", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int shadow
		{
			get
			{
				return this._shadow;
			}
			set
			{
				this._shadow = value;
			}
		}

		[ProtoMember(46, IsRequired = false, Name = "flashWhite", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int flashWhite
		{
			get
			{
				return this._flashWhite;
			}
			set
			{
				this._flashWhite = value;
			}
		}

		[ProtoMember(47, IsRequired = false, Name = "floatBlood", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int floatBlood
		{
			get
			{
				return this._floatBlood;
			}
			set
			{
				this._floatBlood = value;
			}
		}

		[ProtoMember(48, IsRequired = false, Name = "BloodBar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int BloodBar
		{
			get
			{
				return this._BloodBar;
			}
			set
			{
				this._BloodBar = value;
			}
		}

		[ProtoMember(49, IsRequired = false, Name = "talk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int talk
		{
			get
			{
				return this._talk;
			}
			set
			{
				this._talk = value;
			}
		}

		[ProtoMember(50, IsRequired = false, Name = "arrow", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int arrow
		{
			get
			{
				return this._arrow;
			}
			set
			{
				this._arrow = value;
			}
		}

		[ProtoMember(51, IsRequired = false, Name = "invisible", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int invisible
		{
			get
			{
				return this._invisible;
			}
			set
			{
				this._invisible = value;
			}
		}

		[ProtoMember(52, IsRequired = false, Name = "nomoreVisible", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nomoreVisible
		{
			get
			{
				return this._nomoreVisible;
			}
			set
			{
				this._nomoreVisible = value;
			}
		}

		[ProtoMember(53, IsRequired = false, Name = "range", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int range
		{
			get
			{
				return this._range;
			}
			set
			{
				this._range = value;
			}
		}

		[ProtoMember(54, IsRequired = false, Name = "damageCollect", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int damageCollect
		{
			get
			{
				return this._damageCollect;
			}
			set
			{
				this._damageCollect = value;
			}
		}

		[ProtoMember(55, IsRequired = false, Name = "birthAction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int birthAction
		{
			get
			{
				return this._birthAction;
			}
			set
			{
				this._birthAction = value;
			}
		}

		[ProtoMember(56, IsRequired = false, Name = "cameraLock", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cameraLock
		{
			get
			{
				return this._cameraLock;
			}
			set
			{
				this._cameraLock = value;
			}
		}

		[ProtoMember(57, IsRequired = false, Name = "namePic", DataFormat = DataFormat.Default), DefaultValue("")]
		public string namePic
		{
			get
			{
				return this._namePic;
			}
			set
			{
				this._namePic = value;
			}
		}

		[ProtoMember(58, IsRequired = false, Name = "monsterBornDirection", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterBornDirection
		{
			get
			{
				return this._monsterBornDirection;
			}
			set
			{
				this._monsterBornDirection = value;
			}
		}

		[ProtoMember(59, IsRequired = false, Name = "scenePoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int scenePoint
		{
			get
			{
				return this._scenePoint;
			}
			set
			{
				this._scenePoint = value;
			}
		}

		[ProtoMember(60, IsRequired = false, Name = "exclusiveBGM", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int exclusiveBGM
		{
			get
			{
				return this._exclusiveBGM;
			}
			set
			{
				this._exclusiveBGM = value;
			}
		}

		[ProtoMember(61, IsRequired = false, Name = "mode", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		[ProtoMember(62, Name = "triggerCondition", DataFormat = DataFormat.TwosComplement)]
		public List<int> triggerCondition
		{
			get
			{
				return this._triggerCondition;
			}
		}

		[ProtoMember(63, Name = "triggerSkillId", DataFormat = DataFormat.TwosComplement)]
		public List<int> triggerSkillId
		{
			get
			{
				return this._triggerSkillId;
			}
		}

		[ProtoMember(64, IsRequired = false, Name = "prompt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int prompt
		{
			get
			{
				return this._prompt;
			}
			set
			{
				this._prompt = value;
			}
		}

		[ProtoMember(65, IsRequired = false, Name = "HpAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int HpAmplificationRate
		{
			get
			{
				return this._HpAmplificationRate;
			}
			set
			{
				this._HpAmplificationRate = value;
			}
		}

		[ProtoMember(66, IsRequired = false, Name = "AttAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int AttAmplificationRate
		{
			get
			{
				return this._AttAmplificationRate;
			}
			set
			{
				this._AttAmplificationRate = value;
			}
		}

		[ProtoMember(67, IsRequired = false, Name = "DefAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int DefAmplificationRate
		{
			get
			{
				return this._DefAmplificationRate;
			}
			set
			{
				this._DefAmplificationRate = value;
			}
		}

		[ProtoMember(68, IsRequired = false, Name = "HitAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int HitAmplificationRate
		{
			get
			{
				return this._HitAmplificationRate;
			}
			set
			{
				this._HitAmplificationRate = value;
			}
		}

		[ProtoMember(69, IsRequired = false, Name = "DexAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int DexAmplificationRate
		{
			get
			{
				return this._DexAmplificationRate;
			}
			set
			{
				this._DexAmplificationRate = value;
			}
		}

		[ProtoMember(70, IsRequired = false, Name = "CrtAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int CrtAmplificationRate
		{
			get
			{
				return this._CrtAmplificationRate;
			}
			set
			{
				this._CrtAmplificationRate = value;
			}
		}

		[ProtoMember(71, IsRequired = false, Name = "PenAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int PenAmplificationRate
		{
			get
			{
				return this._PenAmplificationRate;
			}
			set
			{
				this._PenAmplificationRate = value;
			}
		}

		[ProtoMember(72, IsRequired = false, Name = "CthAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int CthAmplificationRate
		{
			get
			{
				return this._CthAmplificationRate;
			}
			set
			{
				this._CthAmplificationRate = value;
			}
		}

		[ProtoMember(73, IsRequired = false, Name = "ParAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ParAmplificationRate
		{
			get
			{
				return this._ParAmplificationRate;
			}
			set
			{
				this._ParAmplificationRate = value;
			}
		}

		[ProtoMember(74, IsRequired = false, Name = "VigAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int VigAmplificationRate
		{
			get
			{
				return this._VigAmplificationRate;
			}
			set
			{
				this._VigAmplificationRate = value;
			}
		}

		[ProtoMember(75, IsRequired = false, Name = "PrhAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int PrhAmplificationRate
		{
			get
			{
				return this._PrhAmplificationRate;
			}
			set
			{
				this._PrhAmplificationRate = value;
			}
		}

		[ProtoMember(76, IsRequired = false, Name = "AtsAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int AtsAmplificationRate
		{
			get
			{
				return this._AtsAmplificationRate;
			}
			set
			{
				this._AtsAmplificationRate = value;
			}
		}

		[ProtoMember(77, IsRequired = false, Name = "VpAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int VpAmplificationRate
		{
			get
			{
				return this._VpAmplificationRate;
			}
			set
			{
				this._VpAmplificationRate = value;
			}
		}

		[ProtoMember(78, IsRequired = false, Name = "IvAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int IvAmplificationRate
		{
			get
			{
				return this._IvAmplificationRate;
			}
			set
			{
				this._IvAmplificationRate = value;
			}
		}

		[ProtoMember(79, IsRequired = false, Name = "VsAmplificationRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int VsAmplificationRate
		{
			get
			{
				return this._VsAmplificationRate;
			}
			set
			{
				this._VsAmplificationRate = value;
			}
		}

		[ProtoMember(80, IsRequired = false, Name = "actionPriority", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int actionPriority
		{
			get
			{
				return this._actionPriority;
			}
			set
			{
				this._actionPriority = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
