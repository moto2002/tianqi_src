using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DaLuanDouCanShu")]
	[Serializable]
	public class DaLuanDouCanShu : IExtensible
	{
		private string _field;

		private float _num;

		private string _value = string.Empty;

		private readonly List<int> _scene = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "field", DataFormat = DataFormat.Default)]
		public string field
		{
			get
			{
				return this._field;
			}
			set
			{
				this._field = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "num", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
		public string value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
