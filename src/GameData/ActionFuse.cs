using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"action"
	}), ProtoContract(Name = "ActionFuse")]
	[Serializable]
	public class ActionFuse : IExtensible
	{
		private string _action;

		private float _dieRH;

		private float _dieLH;

		private float _float2;

		private float _float;

		private float _hit;

		private float _hit2;

		private float _attack5;

		private float _attack4;

		private float _attack3;

		private float _attack2;

		private float _attack1;

		private float _run;

		private float _swingback;

		private float _swingleft;

		private float _swingright;

		private float _turnleft;

		private float _turnright;

		private float _idle;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "action", DataFormat = DataFormat.Default)]
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

		[ProtoMember(3, IsRequired = false, Name = "dieRH", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float dieRH
		{
			get
			{
				return this._dieRH;
			}
			set
			{
				this._dieRH = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "dieLH", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float dieLH
		{
			get
			{
				return this._dieLH;
			}
			set
			{
				this._dieLH = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "float2", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float float2
		{
			get
			{
				return this._float2;
			}
			set
			{
				this._float2 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "float", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float @float
		{
			get
			{
				return this._float;
			}
			set
			{
				this._float = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "hit", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float hit
		{
			get
			{
				return this._hit;
			}
			set
			{
				this._hit = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "hit2", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float hit2
		{
			get
			{
				return this._hit2;
			}
			set
			{
				this._hit2 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "attack5", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float attack5
		{
			get
			{
				return this._attack5;
			}
			set
			{
				this._attack5 = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "attack4", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float attack4
		{
			get
			{
				return this._attack4;
			}
			set
			{
				this._attack4 = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "attack3", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float attack3
		{
			get
			{
				return this._attack3;
			}
			set
			{
				this._attack3 = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "attack2", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float attack2
		{
			get
			{
				return this._attack2;
			}
			set
			{
				this._attack2 = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "attack1", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float attack1
		{
			get
			{
				return this._attack1;
			}
			set
			{
				this._attack1 = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "run", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float run
		{
			get
			{
				return this._run;
			}
			set
			{
				this._run = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "swingback", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float swingback
		{
			get
			{
				return this._swingback;
			}
			set
			{
				this._swingback = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "swingleft", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float swingleft
		{
			get
			{
				return this._swingleft;
			}
			set
			{
				this._swingleft = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "swingright", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float swingright
		{
			get
			{
				return this._swingright;
			}
			set
			{
				this._swingright = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "turnleft", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float turnleft
		{
			get
			{
				return this._turnleft;
			}
			set
			{
				this._turnleft = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "turnright", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float turnright
		{
			get
			{
				return this._turnright;
			}
			set
			{
				this._turnright = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "idle", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float idle
		{
			get
			{
				return this._idle;
			}
			set
			{
				this._idle = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
