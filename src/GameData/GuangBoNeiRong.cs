using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GuangBoNeiRong")]
	[Serializable]
	public class GuangBoNeiRong : IExtensible
	{
		private int _id;

		private string _name = string.Empty;

		private int _type;

		private int _desc;

		private readonly List<int> _scene = new List<int>();

		private int _times;

		private int _interval;

		private readonly List<int> _hitEventId = new List<int>();

		private int _show;

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

		[ProtoMember(3, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(5, IsRequired = false, Name = "desc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc = value;
			}
		}

		[ProtoMember(6, Name = "scene", DataFormat = DataFormat.TwosComplement)]
		public List<int> scene
		{
			get
			{
				return this._scene;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "times", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "interval", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int interval
		{
			get
			{
				return this._interval;
			}
			set
			{
				this._interval = value;
			}
		}

		[ProtoMember(9, Name = "hitEventId", DataFormat = DataFormat.TwosComplement)]
		public List<int> hitEventId
		{
			get
			{
				return this._hitEventId;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "show", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int show
		{
			get
			{
				return this._show;
			}
			set
			{
				this._show = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
