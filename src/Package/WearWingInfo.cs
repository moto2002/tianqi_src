using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "WearWingInfo")]
	[Serializable]
	public class WearWingInfo : IExtensible
	{
		private int _wingId;

		private int _lv;

		private bool _wingHidden;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "wingId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int wingId
		{
			get
			{
				return this._wingId;
			}
			set
			{
				this._wingId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "wingHidden", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool wingHidden
		{
			get
			{
				return this._wingHidden;
			}
			set
			{
				this._wingHidden = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
