using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JuQingShiJian")]
	[Serializable]
	public class JuQingShiJian : IExtensible
	{
		private int _parameterId;

		private int _eventType;

		private int _nameId;

		private int _modelId;

		private readonly List<float> _position = new List<float>();

		private readonly List<float> _positionTo = new List<float>();

		private float _moveTime;

		private int _towardAngle;

		private readonly List<string> _action = new List<string>();

		private readonly List<int> _diolague = new List<int>();

		private readonly List<int> _word = new List<int>();

		private int _anime;

		private int _fx;

		private int _voice;

		private int _function;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "parameterId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "eventType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int eventType
		{
			get
			{
				return this._eventType;
			}
			set
			{
				this._eventType = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "nameId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nameId
		{
			get
			{
				return this._nameId;
			}
			set
			{
				this._nameId = value;
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

		[ProtoMember(6, Name = "positionTo", DataFormat = DataFormat.FixedSize)]
		public List<float> positionTo
		{
			get
			{
				return this._positionTo;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "moveTime", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float moveTime
		{
			get
			{
				return this._moveTime;
			}
			set
			{
				this._moveTime = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "towardAngle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, Name = "action", DataFormat = DataFormat.Default)]
		public List<string> action
		{
			get
			{
				return this._action;
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
