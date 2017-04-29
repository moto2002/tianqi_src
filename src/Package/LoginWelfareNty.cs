using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(9932), ForSend(9932), ProtoContract(Name = "LoginWelfareNty")]
	[Serializable]
	public class LoginWelfareNty : IExtensible
	{
		public static readonly short OP = 9932;

		private EveryDayInfo _everyDayInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "everyDayInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public EveryDayInfo everyDayInfo
		{
			get
			{
				return this._everyDayInfo;
			}
			set
			{
				this._everyDayInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
