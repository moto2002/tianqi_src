using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JinJiKaiFangShiJian")]
	[Serializable]
	public class JinJiKaiFangShiJian : IExtensible
	{
		private int _id;

		private string _openTime = string.Empty;

		private int _time;

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

		[ProtoMember(3, IsRequired = false, Name = "openTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string openTime
		{
			get
			{
				return this._openTime;
			}
			set
			{
				this._openTime = value;
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
