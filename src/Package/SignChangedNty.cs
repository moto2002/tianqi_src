using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(587), ForSend(587), ProtoContract(Name = "SignChangedNty")]
	[Serializable]
	public class SignChangedNty : IExtensible
	{
		public static readonly short OP = 587;

		private MonthSignInfo _monthSignInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "monthSignInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public MonthSignInfo monthSignInfo
		{
			get
			{
				return this._monthSignInfo;
			}
			set
			{
				this._monthSignInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
