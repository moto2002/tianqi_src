using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GongHuiRiZhi")]
	[Serializable]
	public class GongHuiRiZhi : IExtensible
	{
		private int _type;

		private string _log = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "log", DataFormat = DataFormat.Default), DefaultValue("")]
		public string log
		{
			get
			{
				return this._log;
			}
			set
			{
				this._log = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
