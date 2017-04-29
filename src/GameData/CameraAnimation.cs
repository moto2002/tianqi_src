using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "CameraAnimation")]
	[Serializable]
	public class CameraAnimation : IExtensible
	{
		private uint _id;

		private int _time;

		private int _aniType;

		private int _type;

		private int _swing;

		private float _decay;

		private float _revolution;

		private float _rate;

		private float _ratio;

		private readonly List<int> _shader = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "aniType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int aniType
		{
			get
			{
				return this._aniType;
			}
			set
			{
				this._aniType = value;
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

		[ProtoMember(7, IsRequired = false, Name = "swing", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int swing
		{
			get
			{
				return this._swing;
			}
			set
			{
				this._swing = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "decay", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float decay
		{
			get
			{
				return this._decay;
			}
			set
			{
				this._decay = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "revolution", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float revolution
		{
			get
			{
				return this._revolution;
			}
			set
			{
				this._revolution = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "rate", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float rate
		{
			get
			{
				return this._rate;
			}
			set
			{
				this._rate = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "ratio", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float ratio
		{
			get
			{
				return this._ratio;
			}
			set
			{
				this._ratio = value;
			}
		}

		[ProtoMember(12, Name = "shader", DataFormat = DataFormat.TwosComplement)]
		public List<int> shader
		{
			get
			{
				return this._shader;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
