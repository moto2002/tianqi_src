using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "CChongWuSheZhi")]
	[Serializable]
	public class CChongWuSheZhi : IExtensible
	{
		private string _field;

		private int _num;

		private string _time = string.Empty;

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

		[ProtoMember(3, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
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

		[ProtoMember(4, IsRequired = false, Name = "time", DataFormat = DataFormat.Default), DefaultValue("")]
		public string time
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
