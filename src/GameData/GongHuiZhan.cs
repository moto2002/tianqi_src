using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GongHuiZhan")]
	[Serializable]
	public class GongHuiZhan : IExtensible
	{
		private string _field;

		private float _num;

		private string _value = string.Empty;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
