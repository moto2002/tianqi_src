using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Fx")]
	[Serializable]
	public class Fx : IExtensible
	{
		private int _id;

		private int _type1;

		private int _isScaleHit;

		private int _type2;

		private string _hook = string.Empty;

		private int _time;

		private string _path = string.Empty;

		private int _audio;

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

		[ProtoMember(4, IsRequired = false, Name = "type1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type1
		{
			get
			{
				return this._type1;
			}
			set
			{
				this._type1 = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "isScaleHit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int isScaleHit
		{
			get
			{
				return this._isScaleHit;
			}
			set
			{
				this._isScaleHit = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "type2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type2
		{
			get
			{
				return this._type2;
			}
			set
			{
				this._type2 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "hook", DataFormat = DataFormat.Default), DefaultValue("")]
		public string hook
		{
			get
			{
				return this._hook;
			}
			set
			{
				this._hook = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "path", DataFormat = DataFormat.Default), DefaultValue("")]
		public string path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "audio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int audio
		{
			get
			{
				return this._audio;
			}
			set
			{
				this._audio = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "listlv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
