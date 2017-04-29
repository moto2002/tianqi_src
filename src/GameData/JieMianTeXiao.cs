using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JieMianTeXiao")]
	[Serializable]
	public class JieMianTeXiao : IExtensible
	{
		private int _id;

		private string _name = string.Empty;

		private int _mountType;

		private int _loop;

		private int _framePerSceond;

		private float _scale;

		private float _offset_x;

		private float _offset_y;

		private float _rotate_z;

		private int _autoRotate;

		private float _angleRotate;

		private int _audioId;

		private int _audioLoop;

		private int _blendWay;

		private float _BrightnessRatio;

		private readonly List<float> _BrightnessRange = new List<float>();

		private readonly List<float> _colour = new List<float>();

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

		[ProtoMember(4, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
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

		[ProtoMember(5, IsRequired = false, Name = "mountType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mountType
		{
			get
			{
				return this._mountType;
			}
			set
			{
				this._mountType = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "loop", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int loop
		{
			get
			{
				return this._loop;
			}
			set
			{
				this._loop = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "framePerSceond", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int framePerSceond
		{
			get
			{
				return this._framePerSceond;
			}
			set
			{
				this._framePerSceond = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "scale", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._scale = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "offset_x", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float offset_x
		{
			get
			{
				return this._offset_x;
			}
			set
			{
				this._offset_x = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "offset_y", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float offset_y
		{
			get
			{
				return this._offset_y;
			}
			set
			{
				this._offset_y = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "rotate_z", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float rotate_z
		{
			get
			{
				return this._rotate_z;
			}
			set
			{
				this._rotate_z = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "autoRotate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int autoRotate
		{
			get
			{
				return this._autoRotate;
			}
			set
			{
				this._autoRotate = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "angleRotate", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float angleRotate
		{
			get
			{
				return this._angleRotate;
			}
			set
			{
				this._angleRotate = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "audioId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(15, IsRequired = false, Name = "audioLoop", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int audioLoop
		{
			get
			{
				return this._audioLoop;
			}
			set
			{
				this._audioLoop = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "blendWay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int blendWay
		{
			get
			{
				return this._blendWay;
			}
			set
			{
				this._blendWay = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "BrightnessRatio", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float BrightnessRatio
		{
			get
			{
				return this._BrightnessRatio;
			}
			set
			{
				this._BrightnessRatio = value;
			}
		}

		[ProtoMember(18, Name = "BrightnessRange", DataFormat = DataFormat.FixedSize)]
		public List<float> BrightnessRange
		{
			get
			{
				return this._BrightnessRange;
			}
		}

		[ProtoMember(19, Name = "colour", DataFormat = DataFormat.FixedSize)]
		public List<float> colour
		{
			get
			{
				return this._colour;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
