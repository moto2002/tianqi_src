using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MonthCardInfo")]
	[Serializable]
	public class MonthCardInfo : IExtensible
	{
		private int _id;

		private bool _hadBuyFlag;

		private int _remainGetTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "hadBuyFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hadBuyFlag
		{
			get
			{
				return this._hadBuyFlag;
			}
			set
			{
				this._hadBuyFlag = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "remainGetTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainGetTimes
		{
			get
			{
				return this._remainGetTimes;
			}
			set
			{
				this._remainGetTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
