using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Pet")]
	[Serializable]
	public class Pet : IExtensible
	{
		[ProtoContract(Name = "SupportskillPair")]
		[Serializable]
		public class SupportskillPair : IExtensible
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

		[ProtoContract(Name = "SkillPair")]
		[Serializable]
		public class SkillPair : IExtensible
		{
			private int _key;

			private string _value = string.Empty;

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

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
			public string value
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

		[ProtoContract(Name = "FuseskillPair")]
		[Serializable]
		public class FuseskillPair : IExtensible
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

		private uint _id;

		private int _hitTips;

		private int _dieTips;

		private int _name;

		private readonly List<int> _model = new List<int>();

		private int _petColour;

		private int _function;

		private readonly List<int> _pic = new List<int>();

		private readonly List<int> _icon = new List<int>();

		private readonly List<int> _icon2 = new List<int>();

		private readonly List<float> _colour = new List<float>();

		private int _summonSkill;

		private readonly List<int> _summonOffset = new List<int>();

		private float _summonEnergy;

		private int _only;

		private int _fuseAP;

		private int _fuseNeedSkill;

		private int _manualSkill;

		private int _manualSkillAP;

		private readonly List<Pet.SupportskillPair> _supportSkill = new List<Pet.SupportskillPair>();

		private int _bornSkill;

		private readonly List<Pet.SkillPair> _skill = new List<Pet.SkillPair>();

		private int _adSupportSkill;

		private int _actionPoint;

		private int _fuseModle;

		private int _fuseNormalSkill;

		private readonly List<Pet.FuseskillPair> _fuseSkill = new List<Pet.FuseskillPair>();

		private readonly List<int> _conditonAI = new List<int>();

		private string _aiId = string.Empty;

		private float _skillInterval;

		private int _interval;

		private readonly List<float> _camRevise = new List<float>();

		private readonly List<float> _camReviseFreedom = new List<float>();

		private readonly List<float> _camManualSkill = new List<float>();

		private int _element;

		private int _readNeedRoleLv;

		private int _fightRoleLv;

		private int _fragmentId;

		private int _initStar;

		private int _maxStar;

		private readonly List<int> _returnFragment = new List<int>();

		private readonly List<int> _keepTime = new List<int>();

		private readonly List<int> _needFragment = new List<int>();

		private readonly List<int> _upstarNeedGold = new List<int>();

		private readonly List<int> _attributeTemplateID = new List<int>();

		private readonly List<int> _attributeTemplateGrowID = new List<int>();

		private readonly List<float> _attributeTemplateFightID = new List<float>();

		private readonly List<float> _attributeTemplateFightGrowID = new List<float>();

		private readonly List<int> _attributeAddToPlayerID = new List<int>();

		private readonly List<int> _attributeAddToPlayerGrowID = new List<int>();

		private readonly List<int> _talent = new List<int>();

		private readonly List<int> _talentStart = new List<int>();

		private readonly List<int> _talentStartLv = new List<int>();

		private readonly List<float> _petEvaluate = new List<float>();

		private int _petType;

		private readonly List<int> _word = new List<int>();

		private float _wordProbability;

		private int _duration;

		private int _interval2;

		private readonly List<float> _zoom = new List<float>();

		private readonly List<int> _modelPreview = new List<int>();

		private int _getTip;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "hitTips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "dieTips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, Name = "model", DataFormat = DataFormat.TwosComplement)]
		public List<int> model
		{
			get
			{
				return this._model;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "petColour", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petColour
		{
			get
			{
				return this._petColour;
			}
			set
			{
				this._petColour = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "function", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int function
		{
			get
			{
				return this._function;
			}
			set
			{
				this._function = value;
			}
		}

		[ProtoMember(10, Name = "pic", DataFormat = DataFormat.TwosComplement)]
		public List<int> pic
		{
			get
			{
				return this._pic;
			}
		}

		[ProtoMember(11, Name = "icon", DataFormat = DataFormat.TwosComplement)]
		public List<int> icon
		{
			get
			{
				return this._icon;
			}
		}

		[ProtoMember(12, Name = "icon2", DataFormat = DataFormat.TwosComplement)]
		public List<int> icon2
		{
			get
			{
				return this._icon2;
			}
		}

		[ProtoMember(13, Name = "colour", DataFormat = DataFormat.FixedSize)]
		public List<float> colour
		{
			get
			{
				return this._colour;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "summonSkill", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int summonSkill
		{
			get
			{
				return this._summonSkill;
			}
			set
			{
				this._summonSkill = value;
			}
		}

		[ProtoMember(15, Name = "summonOffset", DataFormat = DataFormat.TwosComplement)]
		public List<int> summonOffset
		{
			get
			{
				return this._summonOffset;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "summonEnergy", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float summonEnergy
		{
			get
			{
				return this._summonEnergy;
			}
			set
			{
				this._summonEnergy = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "only", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int only
		{
			get
			{
				return this._only;
			}
			set
			{
				this._only = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "fuseAP", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fuseAP
		{
			get
			{
				return this._fuseAP;
			}
			set
			{
				this._fuseAP = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "fuseNeedSkill", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fuseNeedSkill
		{
			get
			{
				return this._fuseNeedSkill;
			}
			set
			{
				this._fuseNeedSkill = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "manualSkill", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int manualSkill
		{
			get
			{
				return this._manualSkill;
			}
			set
			{
				this._manualSkill = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "manualSkillAP", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int manualSkillAP
		{
			get
			{
				return this._manualSkillAP;
			}
			set
			{
				this._manualSkillAP = value;
			}
		}

		[ProtoMember(22, Name = "supportSkill", DataFormat = DataFormat.Default)]
		public List<Pet.SupportskillPair> supportSkill
		{
			get
			{
				return this._supportSkill;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "bornSkill", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bornSkill
		{
			get
			{
				return this._bornSkill;
			}
			set
			{
				this._bornSkill = value;
			}
		}

		[ProtoMember(24, Name = "skill", DataFormat = DataFormat.Default)]
		public List<Pet.SkillPair> skill
		{
			get
			{
				return this._skill;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "adSupportSkill", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int adSupportSkill
		{
			get
			{
				return this._adSupportSkill;
			}
			set
			{
				this._adSupportSkill = value;
			}
		}

		[ProtoMember(26, IsRequired = false, Name = "actionPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(27, IsRequired = false, Name = "fuseModle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fuseModle
		{
			get
			{
				return this._fuseModle;
			}
			set
			{
				this._fuseModle = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "fuseNormalSkill", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fuseNormalSkill
		{
			get
			{
				return this._fuseNormalSkill;
			}
			set
			{
				this._fuseNormalSkill = value;
			}
		}

		[ProtoMember(29, Name = "fuseSkill", DataFormat = DataFormat.Default)]
		public List<Pet.FuseskillPair> fuseSkill
		{
			get
			{
				return this._fuseSkill;
			}
		}

		[ProtoMember(30, Name = "conditonAI", DataFormat = DataFormat.TwosComplement)]
		public List<int> conditonAI
		{
			get
			{
				return this._conditonAI;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "aiId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string aiId
		{
			get
			{
				return this._aiId;
			}
			set
			{
				this._aiId = value;
			}
		}

		[ProtoMember(32, IsRequired = false, Name = "skillInterval", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
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

		[ProtoMember(33, IsRequired = false, Name = "interval", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(34, Name = "camRevise", DataFormat = DataFormat.FixedSize)]
		public List<float> camRevise
		{
			get
			{
				return this._camRevise;
			}
		}

		[ProtoMember(35, Name = "camReviseFreedom", DataFormat = DataFormat.FixedSize)]
		public List<float> camReviseFreedom
		{
			get
			{
				return this._camReviseFreedom;
			}
		}

		[ProtoMember(36, Name = "camManualSkill", DataFormat = DataFormat.FixedSize)]
		public List<float> camManualSkill
		{
			get
			{
				return this._camManualSkill;
			}
		}

		[ProtoMember(37, IsRequired = false, Name = "element", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(38, IsRequired = false, Name = "readNeedRoleLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int readNeedRoleLv
		{
			get
			{
				return this._readNeedRoleLv;
			}
			set
			{
				this._readNeedRoleLv = value;
			}
		}

		[ProtoMember(39, IsRequired = false, Name = "fightRoleLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fightRoleLv
		{
			get
			{
				return this._fightRoleLv;
			}
			set
			{
				this._fightRoleLv = value;
			}
		}

		[ProtoMember(40, IsRequired = false, Name = "fragmentId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fragmentId
		{
			get
			{
				return this._fragmentId;
			}
			set
			{
				this._fragmentId = value;
			}
		}

		[ProtoMember(41, IsRequired = false, Name = "initStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int initStar
		{
			get
			{
				return this._initStar;
			}
			set
			{
				this._initStar = value;
			}
		}

		[ProtoMember(42, IsRequired = false, Name = "maxStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxStar
		{
			get
			{
				return this._maxStar;
			}
			set
			{
				this._maxStar = value;
			}
		}

		[ProtoMember(43, Name = "returnFragment", DataFormat = DataFormat.TwosComplement)]
		public List<int> returnFragment
		{
			get
			{
				return this._returnFragment;
			}
		}

		[ProtoMember(45, Name = "keepTime", DataFormat = DataFormat.TwosComplement)]
		public List<int> keepTime
		{
			get
			{
				return this._keepTime;
			}
		}

		[ProtoMember(46, Name = "needFragment", DataFormat = DataFormat.TwosComplement)]
		public List<int> needFragment
		{
			get
			{
				return this._needFragment;
			}
		}

		[ProtoMember(47, Name = "upstarNeedGold", DataFormat = DataFormat.TwosComplement)]
		public List<int> upstarNeedGold
		{
			get
			{
				return this._upstarNeedGold;
			}
		}

		[ProtoMember(48, Name = "attributeTemplateID", DataFormat = DataFormat.TwosComplement)]
		public List<int> attributeTemplateID
		{
			get
			{
				return this._attributeTemplateID;
			}
		}

		[ProtoMember(49, Name = "attributeTemplateGrowID", DataFormat = DataFormat.TwosComplement)]
		public List<int> attributeTemplateGrowID
		{
			get
			{
				return this._attributeTemplateGrowID;
			}
		}

		[ProtoMember(50, Name = "attributeTemplateFightID", DataFormat = DataFormat.FixedSize)]
		public List<float> attributeTemplateFightID
		{
			get
			{
				return this._attributeTemplateFightID;
			}
		}

		[ProtoMember(51, Name = "attributeTemplateFightGrowID", DataFormat = DataFormat.FixedSize)]
		public List<float> attributeTemplateFightGrowID
		{
			get
			{
				return this._attributeTemplateFightGrowID;
			}
		}

		[ProtoMember(52, Name = "attributeAddToPlayerID", DataFormat = DataFormat.TwosComplement)]
		public List<int> attributeAddToPlayerID
		{
			get
			{
				return this._attributeAddToPlayerID;
			}
		}

		[ProtoMember(53, Name = "attributeAddToPlayerGrowID", DataFormat = DataFormat.TwosComplement)]
		public List<int> attributeAddToPlayerGrowID
		{
			get
			{
				return this._attributeAddToPlayerGrowID;
			}
		}

		[ProtoMember(54, Name = "talent", DataFormat = DataFormat.TwosComplement)]
		public List<int> talent
		{
			get
			{
				return this._talent;
			}
		}

		[ProtoMember(55, Name = "talentStart", DataFormat = DataFormat.TwosComplement)]
		public List<int> talentStart
		{
			get
			{
				return this._talentStart;
			}
		}

		[ProtoMember(56, Name = "talentStartLv", DataFormat = DataFormat.TwosComplement)]
		public List<int> talentStartLv
		{
			get
			{
				return this._talentStartLv;
			}
		}

		[ProtoMember(57, Name = "petEvaluate", DataFormat = DataFormat.FixedSize)]
		public List<float> petEvaluate
		{
			get
			{
				return this._petEvaluate;
			}
		}

		[ProtoMember(58, IsRequired = false, Name = "petType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petType
		{
			get
			{
				return this._petType;
			}
			set
			{
				this._petType = value;
			}
		}

		[ProtoMember(59, Name = "word", DataFormat = DataFormat.TwosComplement)]
		public List<int> word
		{
			get
			{
				return this._word;
			}
		}

		[ProtoMember(60, IsRequired = false, Name = "wordProbability", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float wordProbability
		{
			get
			{
				return this._wordProbability;
			}
			set
			{
				this._wordProbability = value;
			}
		}

		[ProtoMember(61, IsRequired = false, Name = "duration", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(62, IsRequired = false, Name = "interval2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int interval2
		{
			get
			{
				return this._interval2;
			}
			set
			{
				this._interval2 = value;
			}
		}

		[ProtoMember(63, Name = "zoom", DataFormat = DataFormat.FixedSize)]
		public List<float> zoom
		{
			get
			{
				return this._zoom;
			}
		}

		[ProtoMember(64, Name = "modelPreview", DataFormat = DataFormat.TwosComplement)]
		public List<int> modelPreview
		{
			get
			{
				return this._modelPreview;
			}
		}

		[ProtoMember(65, IsRequired = false, Name = "getTip", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int getTip
		{
			get
			{
				return this._getTip;
			}
			set
			{
				this._getTip = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
