using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "CaiJiPeiZhi")]
	[Serializable]
	public class CaiJiPeiZhi : IExtensible
	{
		private int _id;

		private int _type;

		private int _name;

		private int _model;

		private int _scene;

		private readonly List<int> _position = new List<int>();

		private readonly List<int> _face = new List<int>();

		private readonly List<int> _frontMainTask = new List<int>();

		private readonly List<int> _behindMainTask = new List<int>();

		private readonly List<int> _flyPosition = new List<int>();

		private readonly List<int> _touchRange = new List<int>();

		private readonly List<int> _triggeredRange = new List<int>();

		private int _tips;

		private string _action = string.Empty;

		private int _time;

		private string _action2 = string.Empty;

		private string _specialEffects = string.Empty;

		private readonly List<int> _position2 = new List<int>();

		private readonly List<string> _LockLookPoint = new List<string>();

		private int _probability;

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

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "scene", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int scene
		{
			get
			{
				return this._scene;
			}
			set
			{
				this._scene = value;
			}
		}

		[ProtoMember(7, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public List<int> position
		{
			get
			{
				return this._position;
			}
		}

		[ProtoMember(8, Name = "face", DataFormat = DataFormat.TwosComplement)]
		public List<int> face
		{
			get
			{
				return this._face;
			}
		}

		[ProtoMember(9, Name = "frontMainTask", DataFormat = DataFormat.TwosComplement)]
		public List<int> frontMainTask
		{
			get
			{
				return this._frontMainTask;
			}
		}

		[ProtoMember(10, Name = "behindMainTask", DataFormat = DataFormat.TwosComplement)]
		public List<int> behindMainTask
		{
			get
			{
				return this._behindMainTask;
			}
		}

		[ProtoMember(11, Name = "flyPosition", DataFormat = DataFormat.TwosComplement)]
		public List<int> flyPosition
		{
			get
			{
				return this._flyPosition;
			}
		}

		[ProtoMember(12, Name = "touchRange", DataFormat = DataFormat.TwosComplement)]
		public List<int> touchRange
		{
			get
			{
				return this._touchRange;
			}
		}

		[ProtoMember(13, Name = "triggeredRange", DataFormat = DataFormat.TwosComplement)]
		public List<int> triggeredRange
		{
			get
			{
				return this._triggeredRange;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "tips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tips
		{
			get
			{
				return this._tips;
			}
			set
			{
				this._tips = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "action", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(16, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(17, IsRequired = false, Name = "action2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string action2
		{
			get
			{
				return this._action2;
			}
			set
			{
				this._action2 = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "specialEffects", DataFormat = DataFormat.Default), DefaultValue("")]
		public string specialEffects
		{
			get
			{
				return this._specialEffects;
			}
			set
			{
				this._specialEffects = value;
			}
		}

		[ProtoMember(19, Name = "position2", DataFormat = DataFormat.TwosComplement)]
		public List<int> position2
		{
			get
			{
				return this._position2;
			}
		}

		[ProtoMember(20, Name = "LockLookPoint", DataFormat = DataFormat.Default)]
		public List<string> LockLookPoint
		{
			get
			{
				return this._LockLookPoint;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
