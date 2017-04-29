using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "NPC")]
	[Serializable]
	public class NPC : IExtensible
	{
		private int _id;

		private int _name;

		private int _model;

		private int _pic;

		private int _scene;

		private readonly List<int> _position = new List<int>();

		private readonly List<int> _face = new List<int>();

		private readonly List<int> _word = new List<int>();

		private readonly List<int> _sound = new List<int>();

		private readonly List<int> _frontMainTask = new List<int>();

		private readonly List<int> _behindMainTask = new List<int>();

		private int _sameNpc;

		private int _relevancyNpc;

		private readonly List<int> _touchRange = new List<int>();

		private readonly List<int> _triggeredRange = new List<int>();

		private readonly List<int> _clickRange = new List<int>();

		private int _turn;

		private readonly List<int> _function = new List<int>();

		private int _mapPic;

		private string _action = string.Empty;

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

		[ProtoMember(3, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "pic", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, Name = "word", DataFormat = DataFormat.TwosComplement)]
		public List<int> word
		{
			get
			{
				return this._word;
			}
		}

		[ProtoMember(10, Name = "sound", DataFormat = DataFormat.TwosComplement)]
		public List<int> sound
		{
			get
			{
				return this._sound;
			}
		}

		[ProtoMember(11, Name = "frontMainTask", DataFormat = DataFormat.TwosComplement)]
		public List<int> frontMainTask
		{
			get
			{
				return this._frontMainTask;
			}
		}

		[ProtoMember(12, Name = "behindMainTask", DataFormat = DataFormat.TwosComplement)]
		public List<int> behindMainTask
		{
			get
			{
				return this._behindMainTask;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "sameNpc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sameNpc
		{
			get
			{
				return this._sameNpc;
			}
			set
			{
				this._sameNpc = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "relevancyNpc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int relevancyNpc
		{
			get
			{
				return this._relevancyNpc;
			}
			set
			{
				this._relevancyNpc = value;
			}
		}

		[ProtoMember(15, Name = "touchRange", DataFormat = DataFormat.TwosComplement)]
		public List<int> touchRange
		{
			get
			{
				return this._touchRange;
			}
		}

		[ProtoMember(16, Name = "triggeredRange", DataFormat = DataFormat.TwosComplement)]
		public List<int> triggeredRange
		{
			get
			{
				return this._triggeredRange;
			}
		}

		[ProtoMember(17, Name = "clickRange", DataFormat = DataFormat.TwosComplement)]
		public List<int> clickRange
		{
			get
			{
				return this._clickRange;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "turn", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int turn
		{
			get
			{
				return this._turn;
			}
			set
			{
				this._turn = value;
			}
		}

		[ProtoMember(19, Name = "function", DataFormat = DataFormat.TwosComplement)]
		public List<int> function
		{
			get
			{
				return this._function;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "mapPic", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mapPic
		{
			get
			{
				return this._mapPic;
			}
			set
			{
				this._mapPic = value;
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
