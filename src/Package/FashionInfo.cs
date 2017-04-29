using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "FashionInfo")]
	[Serializable]
	public class FashionInfo : IExtensible
	{
		private string _fashionId;

		private bool _isWear;

		private int _times;

		private bool _have;

		private bool _overFlag;

		private int _effectType;

		private bool _hasEffect;

		private bool _hasNotice;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "fashionId", DataFormat = DataFormat.Default)]
		public string fashionId
		{
			get
			{
				return this._fashionId;
			}
			set
			{
				this._fashionId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "isWear", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isWear
		{
			get
			{
				return this._isWear;
			}
			set
			{
				this._isWear = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "times", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "have", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool have
		{
			get
			{
				return this._have;
			}
			set
			{
				this._have = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "overFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool overFlag
		{
			get
			{
				return this._overFlag;
			}
			set
			{
				this._overFlag = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "effectType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int effectType
		{
			get
			{
				return this._effectType;
			}
			set
			{
				this._effectType = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "hasEffect", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hasEffect
		{
			get
			{
				return this._hasEffect;
			}
			set
			{
				this._hasEffect = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "hasNotice", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hasNotice
		{
			get
			{
				return this._hasNotice;
			}
			set
			{
				this._hasNotice = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
