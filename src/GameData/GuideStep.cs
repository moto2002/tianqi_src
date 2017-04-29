using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "GuideStep")]
	[Serializable]
	public class GuideStep : IExtensible
	{
		private int _id;

		private int _group;

		private int _step;

		private int _step_record;

		private int _step_finish;

		private int _type;

		private int _direction;

		private int _instructionId;

		private readonly List<float> _pos = new List<float>();

		private int _triggerUI;

		private int _widgetId;

		private int _lightWidgetId;

		private int _systemId;

		private int _lockMode;

		private readonly List<int> _dynamicWidgets = new List<int>();

		private readonly List<int> _successMode = new List<int>();

		private float _minTime;

		private float _liveTime;

		private readonly List<int> _effectId = new List<int>();

		private int _audioId;

		private int _skipPosition;

		private string _picture = string.Empty;

		private int _listDragOn;

		private IExtension extensionObject;

		[ProtoMember(4, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = false, Name = "group", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "step", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "step_record", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int step_record
		{
			get
			{
				return this._step_record;
			}
			set
			{
				this._step_record = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "step_finish", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int step_finish
		{
			get
			{
				return this._step_finish;
			}
			set
			{
				this._step_finish = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "direction", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int direction
		{
			get
			{
				return this._direction;
			}
			set
			{
				this._direction = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "instructionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int instructionId
		{
			get
			{
				return this._instructionId;
			}
			set
			{
				this._instructionId = value;
			}
		}

		[ProtoMember(12, Name = "pos", DataFormat = DataFormat.FixedSize)]
		public List<float> pos
		{
			get
			{
				return this._pos;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "triggerUI", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int triggerUI
		{
			get
			{
				return this._triggerUI;
			}
			set
			{
				this._triggerUI = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "widgetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int widgetId
		{
			get
			{
				return this._widgetId;
			}
			set
			{
				this._widgetId = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "lightWidgetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lightWidgetId
		{
			get
			{
				return this._lightWidgetId;
			}
			set
			{
				this._lightWidgetId = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "systemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int systemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				this._systemId = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "lockMode", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lockMode
		{
			get
			{
				return this._lockMode;
			}
			set
			{
				this._lockMode = value;
			}
		}

		[ProtoMember(18, Name = "dynamicWidgets", DataFormat = DataFormat.TwosComplement)]
		public List<int> dynamicWidgets
		{
			get
			{
				return this._dynamicWidgets;
			}
		}

		[ProtoMember(19, Name = "successMode", DataFormat = DataFormat.TwosComplement)]
		public List<int> successMode
		{
			get
			{
				return this._successMode;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "minTime", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float minTime
		{
			get
			{
				return this._minTime;
			}
			set
			{
				this._minTime = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "liveTime", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float liveTime
		{
			get
			{
				return this._liveTime;
			}
			set
			{
				this._liveTime = value;
			}
		}

		[ProtoMember(22, Name = "effectId", DataFormat = DataFormat.TwosComplement)]
		public List<int> effectId
		{
			get
			{
				return this._effectId;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "audioId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int audioId
		{
			get
			{
				return this._audioId;
			}
			set
			{
				this._audioId = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "skipPosition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skipPosition
		{
			get
			{
				return this._skipPosition;
			}
			set
			{
				this._skipPosition = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "picture", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(26, IsRequired = false, Name = "listDragOn", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int listDragOn
		{
			get
			{
				return this._listDragOn;
			}
			set
			{
				this._listDragOn = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
