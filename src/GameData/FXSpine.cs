using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "FXSpine")]
	[Serializable]
	public class FXSpine : IExtensible
	{
		private int _id;

		private string _name = string.Empty;

		private string _anim = string.Empty;

		private int _audioId;

		private int _audioLoop;

		private int _blendWay;

		private float _BrightnessRatio;

		private int _rgbEffect;

		private int _stayLastFrame;

		private int _listlv;

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

		[ProtoMember(5, IsRequired = false, Name = "anim", DataFormat = DataFormat.Default), DefaultValue("")]
		public string anim
		{
			get
			{
				return this._anim;
			}
			set
			{
				this._anim = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "audioId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "audioLoop", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "blendWay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "BrightnessRatio", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
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

		[ProtoMember(10, IsRequired = false, Name = "rgbEffect", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rgbEffect
		{
			get
			{
				return this._rgbEffect;
			}
			set
			{
				this._rgbEffect = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "stayLastFrame", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int stayLastFrame
		{
			get
			{
				return this._stayLastFrame;
			}
			set
			{
				this._stayLastFrame = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "listlv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int listlv
		{
			get
			{
				return this._listlv;
			}
			set
			{
				this._listlv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
