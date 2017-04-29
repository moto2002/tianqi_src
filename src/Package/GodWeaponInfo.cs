using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GodWeaponInfo")]
	[Serializable]
	public class GodWeaponInfo : IExtensible
	{
		private int _Type;

		private bool _isOpen;

		private int _gLevel;

		private int _gExp;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "Type", DataFormat = DataFormat.TwosComplement)]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "isOpen", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isOpen
		{
			get
			{
				return this._isOpen;
			}
			set
			{
				this._isOpen = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "gLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gLevel
		{
			get
			{
				return this._gLevel;
			}
			set
			{
				this._gLevel = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "gExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gExp
		{
			get
			{
				return this._gExp;
			}
			set
			{
				this._gExp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
