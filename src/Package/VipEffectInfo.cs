using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "VipEffectInfo")]
	[Serializable]
	public class VipEffectInfo : IExtensible
	{
		private int _vipLv;

		private int _effectId;

		private bool _opened;

		private bool _boxOpened;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "vipLv", DataFormat = DataFormat.TwosComplement)]
		public int vipLv
		{
			get
			{
				return this._vipLv;
			}
			set
			{
				this._vipLv = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "effectId", DataFormat = DataFormat.TwosComplement)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "opened", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool opened
		{
			get
			{
				return this._opened;
			}
			set
			{
				this._opened = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "boxOpened", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool boxOpened
		{
			get
			{
				return this._boxOpened;
			}
			set
			{
				this._boxOpened = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
