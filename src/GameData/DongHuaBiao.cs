using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DongHuaBiao")]
	[Serializable]
	public class DongHuaBiao : IExtensible
	{
		private int _id;

		private int _type;

		private int _time;

		private int _npcId;

		private readonly List<string> _action = new List<string>();

		private readonly List<float> _posA = new List<float>();

		private readonly List<float> _posB = new List<float>();

		private int _towardA;

		private int _towardB;

		private int _task;

		private readonly List<float> _cameraA = new List<float>();

		private readonly List<float> _cameraB = new List<float>();

		private int _word;

		private string _animation = string.Empty;

		private int _wink;

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

		[ProtoMember(4, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "npcId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int npcId
		{
			get
			{
				return this._npcId;
			}
			set
			{
				this._npcId = value;
			}
		}

		[ProtoMember(7, Name = "action", DataFormat = DataFormat.Default)]
		public List<string> action
		{
			get
			{
				return this._action;
			}
		}

		[ProtoMember(8, Name = "posA", DataFormat = DataFormat.FixedSize)]
		public List<float> posA
		{
			get
			{
				return this._posA;
			}
		}

		[ProtoMember(9, Name = "posB", DataFormat = DataFormat.FixedSize)]
		public List<float> posB
		{
			get
			{
				return this._posB;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "towardA", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int towardA
		{
			get
			{
				return this._towardA;
			}
			set
			{
				this._towardA = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "towardB", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int towardB
		{
			get
			{
				return this._towardB;
			}
			set
			{
				this._towardB = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "task", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int task
		{
			get
			{
				return this._task;
			}
			set
			{
				this._task = value;
			}
		}

		[ProtoMember(13, Name = "cameraA", DataFormat = DataFormat.FixedSize)]
		public List<float> cameraA
		{
			get
			{
				return this._cameraA;
			}
		}

		[ProtoMember(14, Name = "cameraB", DataFormat = DataFormat.FixedSize)]
		public List<float> cameraB
		{
			get
			{
				return this._cameraB;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "word", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int word
		{
			get
			{
				return this._word;
			}
			set
			{
				this._word = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "animation", DataFormat = DataFormat.Default), DefaultValue("")]
		public string animation
		{
			get
			{
				return this._animation;
			}
			set
			{
				this._animation = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "wink", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int wink
		{
			get
			{
				return this._wink;
			}
			set
			{
				this._wink = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
