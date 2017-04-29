using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShiJianCanShuBiao")]
	[Serializable]
	public class ShiJianCanShuBiao : IExtensible
	{
		private int _parameterId;

		private int _modelId;

		private readonly List<float> _position = new List<float>();

		private readonly List<float> _towardPoint = new List<float>();

		private int _towardAngle;

		private string _action = string.Empty;

		private int _actionTime;

		private readonly List<int> _diolague = new List<int>();

		private readonly List<int> _word = new List<int>();

		private int _anime;

		private int _fx;

		private int _voice;

		private int _function;

		private string _anime2 = string.Empty;

		private int _isManualContinue;

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "parameterId", DataFormat = DataFormat.TwosComplement)]
		public int parameterId
		{
			get
			{
				return this._parameterId;
			}
			set
			{
				this._parameterId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "modelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		[ProtoMember(5, Name = "position", DataFormat = DataFormat.FixedSize)]
		public List<float> position
		{
			get
			{
				return this._position;
			}
		}

		[ProtoMember(6, Name = "towardPoint", DataFormat = DataFormat.FixedSize)]
		public List<float> towardPoint
		{
			get
			{
				return this._towardPoint;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "towardAngle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int towardAngle
		{
			get
			{
				return this._towardAngle;
			}
			set
			{
				this._towardAngle = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "action", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(9, IsRequired = false, Name = "actionTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int actionTime
		{
			get
			{
				return this._actionTime;
			}
			set
			{
				this._actionTime = value;
			}
		}

		[ProtoMember(10, Name = "diolague", DataFormat = DataFormat.TwosComplement)]
		public List<int> diolague
		{
			get
			{
				return this._diolague;
			}
		}

		[ProtoMember(11, Name = "word", DataFormat = DataFormat.TwosComplement)]
		public List<int> word
		{
			get
			{
				return this._word;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "anime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int anime
		{
			get
			{
				return this._anime;
			}
			set
			{
				this._anime = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "fx", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(14, IsRequired = false, Name = "voice", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int voice
		{
			get
			{
				return this._voice;
			}
			set
			{
				this._voice = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "function", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(16, IsRequired = false, Name = "anime2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string anime2
		{
			get
			{
				return this._anime2;
			}
			set
			{
				this._anime2 = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "isManualContinue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int isManualContinue
		{
			get
			{
				return this._isManualContinue;
			}
			set
			{
				this._isManualContinue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
