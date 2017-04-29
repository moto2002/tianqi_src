using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Buff")]
	[Serializable]
	public class Buff : IExtensible
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

		[ProtoContract(Name = "RoletargetpropidPair")]
		[Serializable]
		public class RoletargetpropidPair : IExtensible
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

		private readonly List<int> _compound = new List<int>();

		private int _text;

		private int _type;

		private readonly List<int> _damageType = new List<int>();

		private int _time;

		private int _interval;

		private string _action = string.Empty;

		private readonly List<int> _fx = new List<int>();

		private int _tempSkill;

		private int _effect;

		private readonly List<int> _resistId = new List<int>();

		private readonly List<int> _coverId = new List<int>();

		private int _immediateEffect;

		private int _overlayModeId;

		private readonly List<int> _resist = new List<int>();

		private readonly List<int> _cover = new List<int>();

		private readonly List<int> _delete = new List<int>();

		private int _shader;

		private int _buffProp;

		private int _forceHandle;

		private int _globalTarget;

		private int _propId;

		private int _targetPropId;

		private readonly List<Buff.RolepropidPair> _rolePropId = new List<Buff.RolepropidPair>();

		private readonly List<Buff.RoletargetpropidPair> _roleTargetPropId = new List<Buff.RoletargetpropidPair>();

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

		[ProtoMember(4, Name = "compound", DataFormat = DataFormat.TwosComplement)]
		public List<int> compound
		{
			get
			{
				return this._compound;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "text", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(7, Name = "damageType", DataFormat = DataFormat.TwosComplement)]
		public List<int> damageType
		{
			get
			{
				return this._damageType;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "interval", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "action", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(11, Name = "fx", DataFormat = DataFormat.TwosComplement)]
		public List<int> fx
		{
			get
			{
				return this._fx;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "tempSkill", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tempSkill
		{
			get
			{
				return this._tempSkill;
			}
			set
			{
				this._tempSkill = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "effect", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int effect
		{
			get
			{
				return this._effect;
			}
			set
			{
				this._effect = value;
			}
		}

		[ProtoMember(14, Name = "resistId", DataFormat = DataFormat.TwosComplement)]
		public List<int> resistId
		{
			get
			{
				return this._resistId;
			}
		}

		[ProtoMember(15, Name = "coverId", DataFormat = DataFormat.TwosComplement)]
		public List<int> coverId
		{
			get
			{
				return this._coverId;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "immediateEffect", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int immediateEffect
		{
			get
			{
				return this._immediateEffect;
			}
			set
			{
				this._immediateEffect = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "overlayModeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int overlayModeId
		{
			get
			{
				return this._overlayModeId;
			}
			set
			{
				this._overlayModeId = value;
			}
		}

		[ProtoMember(18, Name = "resist", DataFormat = DataFormat.TwosComplement)]
		public List<int> resist
		{
			get
			{
				return this._resist;
			}
		}

		[ProtoMember(19, Name = "cover", DataFormat = DataFormat.TwosComplement)]
		public List<int> cover
		{
			get
			{
				return this._cover;
			}
		}

		[ProtoMember(20, Name = "delete", DataFormat = DataFormat.TwosComplement)]
		public List<int> delete
		{
			get
			{
				return this._delete;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "shader", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int shader
		{
			get
			{
				return this._shader;
			}
			set
			{
				this._shader = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "buffProp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int buffProp
		{
			get
			{
				return this._buffProp;
			}
			set
			{
				this._buffProp = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "forceHandle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int forceHandle
		{
			get
			{
				return this._forceHandle;
			}
			set
			{
				this._forceHandle = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "globalTarget", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int globalTarget
		{
			get
			{
				return this._globalTarget;
			}
			set
			{
				this._globalTarget = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "propId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(26, IsRequired = false, Name = "targetPropId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetPropId
		{
			get
			{
				return this._targetPropId;
			}
			set
			{
				this._targetPropId = value;
			}
		}

		[ProtoMember(27, Name = "rolePropId", DataFormat = DataFormat.Default)]
		public List<Buff.RolepropidPair> rolePropId
		{
			get
			{
				return this._rolePropId;
			}
		}

		[ProtoMember(28, Name = "roleTargetPropId", DataFormat = DataFormat.Default)]
		public List<Buff.RoletargetpropidPair> roleTargetPropId
		{
			get
			{
				return this._roleTargetPropId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
