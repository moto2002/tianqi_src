using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Skill")]
	[Serializable]
	public class Skill : IExtensible
	{
		[ProtoContract(Name = "GroupcdPair")]
		[Serializable]
		public class GroupcdPair : IExtensible
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

		private int _icon;

		private int _skilltype;

		private int _type1;

		private int _type2;

		private int _type3;

		private int _name;

		private int _describeId;

		private int _conditionId;

		private int _aiSkillMove;

		private readonly List<int> _dynamicWarningArea = new List<int>();

		private readonly List<int> _dynamicWarningAreaOffset = new List<int>();

		private int _skillWarningTime;

		private int _targetType;

		private int _actionPoint;

		private int _cd;

		private int _combo;

		private readonly List<int> _effect = new List<int>();

		private readonly List<int> _fx = new List<int>();

		private string _attAction = string.Empty;

		private int _actionPriority;

		private int _antiaircraft;

		private int _rush;

		private string _eventTag = string.Empty;

		private int _autoAim;

		private float _reachLimit;

		private readonly List<int> _reach = new List<int>();

		private readonly List<int> _angle = new List<int>();

		private readonly List<float> _cameraCorrection_x = new List<float>();

		private int _talk;

		private readonly List<int> _getTarget = new List<int>();

		private int _initCd;

		private readonly List<Skill.GroupcdPair> _groupCd = new List<Skill.GroupcdPair>();

		private int _group;

		private int _equivalentLv;

		private int _superArmor;

		private float _changeSpeed;

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

		[ProtoMember(4, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "skilltype", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skilltype
		{
			get
			{
				return this._skilltype;
			}
			set
			{
				this._skilltype = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "type1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "type2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "type3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type3
		{
			get
			{
				return this._type3;
			}
			set
			{
				this._type3 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "describeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describeId
		{
			get
			{
				return this._describeId;
			}
			set
			{
				this._describeId = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "conditionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int conditionId
		{
			get
			{
				return this._conditionId;
			}
			set
			{
				this._conditionId = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "aiSkillMove", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int aiSkillMove
		{
			get
			{
				return this._aiSkillMove;
			}
			set
			{
				this._aiSkillMove = value;
			}
		}

		[ProtoMember(13, Name = "dynamicWarningArea", DataFormat = DataFormat.TwosComplement)]
		public List<int> dynamicWarningArea
		{
			get
			{
				return this._dynamicWarningArea;
			}
		}

		[ProtoMember(14, Name = "dynamicWarningAreaOffset", DataFormat = DataFormat.TwosComplement)]
		public List<int> dynamicWarningAreaOffset
		{
			get
			{
				return this._dynamicWarningAreaOffset;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "skillWarningTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillWarningTime
		{
			get
			{
				return this._skillWarningTime;
			}
			set
			{
				this._skillWarningTime = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "targetType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(18, IsRequired = false, Name = "cd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cd
		{
			get
			{
				return this._cd;
			}
			set
			{
				this._cd = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "combo", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int combo
		{
			get
			{
				return this._combo;
			}
			set
			{
				this._combo = value;
			}
		}

		[ProtoMember(20, Name = "effect", DataFormat = DataFormat.TwosComplement)]
		public List<int> effect
		{
			get
			{
				return this._effect;
			}
		}

		[ProtoMember(21, Name = "fx", DataFormat = DataFormat.TwosComplement)]
		public List<int> fx
		{
			get
			{
				return this._fx;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "attAction", DataFormat = DataFormat.Default), DefaultValue("")]
		public string attAction
		{
			get
			{
				return this._attAction;
			}
			set
			{
				this._attAction = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "actionPriority", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(24, IsRequired = false, Name = "antiaircraft", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(25, IsRequired = false, Name = "rush", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rush
		{
			get
			{
				return this._rush;
			}
			set
			{
				this._rush = value;
			}
		}

		[ProtoMember(26, IsRequired = false, Name = "eventTag", DataFormat = DataFormat.Default), DefaultValue("")]
		public string eventTag
		{
			get
			{
				return this._eventTag;
			}
			set
			{
				this._eventTag = value;
			}
		}

		[ProtoMember(27, IsRequired = false, Name = "autoAim", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int autoAim
		{
			get
			{
				return this._autoAim;
			}
			set
			{
				this._autoAim = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "reachLimit", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float reachLimit
		{
			get
			{
				return this._reachLimit;
			}
			set
			{
				this._reachLimit = value;
			}
		}

		[ProtoMember(29, Name = "reach", DataFormat = DataFormat.TwosComplement)]
		public List<int> reach
		{
			get
			{
				return this._reach;
			}
		}

		[ProtoMember(30, Name = "angle", DataFormat = DataFormat.TwosComplement)]
		public List<int> angle
		{
			get
			{
				return this._angle;
			}
		}

		[ProtoMember(31, Name = "cameraCorrection_x", DataFormat = DataFormat.FixedSize)]
		public List<float> cameraCorrection_x
		{
			get
			{
				return this._cameraCorrection_x;
			}
		}

		[ProtoMember(32, IsRequired = false, Name = "talk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(33, Name = "getTarget", DataFormat = DataFormat.TwosComplement)]
		public List<int> getTarget
		{
			get
			{
				return this._getTarget;
			}
		}

		[ProtoMember(34, IsRequired = false, Name = "initCd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int initCd
		{
			get
			{
				return this._initCd;
			}
			set
			{
				this._initCd = value;
			}
		}

		[ProtoMember(35, Name = "groupCd", DataFormat = DataFormat.Default)]
		public List<Skill.GroupcdPair> groupCd
		{
			get
			{
				return this._groupCd;
			}
		}

		[ProtoMember(36, IsRequired = false, Name = "group", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int group
		{
			get
			{
				return this._group;
			}
			set
			{
				this._group = value;
			}
		}

		[ProtoMember(37, IsRequired = false, Name = "equivalentLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equivalentLv
		{
			get
			{
				return this._equivalentLv;
			}
			set
			{
				this._equivalentLv = value;
			}
		}

		[ProtoMember(38, IsRequired = false, Name = "superArmor", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int superArmor
		{
			get
			{
				return this._superArmor;
			}
			set
			{
				this._superArmor = value;
			}
		}

		[ProtoMember(39, IsRequired = false, Name = "changeSpeed", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float changeSpeed
		{
			get
			{
				return this._changeSpeed;
			}
			set
			{
				this._changeSpeed = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
