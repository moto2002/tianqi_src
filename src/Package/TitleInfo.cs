using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "TitleInfo")]
	[Serializable]
	public class TitleInfo : IExtensible
	{
		private int _titleId;

		private int _remainTime;

		private bool _lookFlag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "titleId", DataFormat = DataFormat.TwosComplement)]
		public int titleId
		{
			get
			{
				return this._titleId;
			}
			set
			{
				this._titleId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "remainTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainTime
		{
			get
			{
				return this._remainTime;
			}
			set
			{
				this._remainTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "lookFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool lookFlag
		{
			get
			{
				return this._lookFlag;
			}
			set
			{
				this._lookFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
