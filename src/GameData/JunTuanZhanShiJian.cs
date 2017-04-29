using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanShiJian")]
	[Serializable]
	public class JunTuanZhanShiJian : IExtensible
	{
		private int _id;

		private string _OpenTime = string.Empty;

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

		[ProtoMember(3, IsRequired = false, Name = "OpenTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string OpenTime
		{
			get
			{
				return this._OpenTime;
			}
			set
			{
				this._OpenTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
