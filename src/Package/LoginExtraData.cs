using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7672), ForSend(7672), ProtoContract(Name = "LoginExtraData")]
	[Serializable]
	public class LoginExtraData : IExtensible
	{
		public static readonly short OP = 7672;

		private string _deviceModel = string.Empty;

		private string _deviceSys = string.Empty;

		private string _deviceId = string.Empty;

		private string _imei = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "deviceModel", DataFormat = DataFormat.Default), DefaultValue("")]
		public string deviceModel
		{
			get
			{
				return this._deviceModel;
			}
			set
			{
				this._deviceModel = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "deviceSys", DataFormat = DataFormat.Default), DefaultValue("")]
		public string deviceSys
		{
			get
			{
				return this._deviceSys;
			}
			set
			{
				this._deviceSys = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "deviceId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string deviceId
		{
			get
			{
				return this._deviceId;
			}
			set
			{
				this._deviceId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "imei", DataFormat = DataFormat.Default), DefaultValue("")]
		public string imei
		{
			get
			{
				return this._imei;
			}
			set
			{
				this._imei = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
