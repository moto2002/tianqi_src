using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "RenWuMuBiao")]
	[Serializable]
	public class RenWuMuBiao : IExtensible
	{
		private int _key;

		private string _value = string.Empty;

		private readonly List<int> _prizeIcon = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
		public int key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(4, Name = "prizeIcon", DataFormat = DataFormat.TwosComplement)]
		public List<int> prizeIcon
		{
			get
			{
				return this._prizeIcon;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
