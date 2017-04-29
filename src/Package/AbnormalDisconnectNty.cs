using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(937), ForSend(937), ProtoContract(Name = "AbnormalDisconnectNty")]
	[Serializable]
	public class AbnormalDisconnectNty : IExtensible
	{
		[ProtoContract(Name = "AbnormalType")]
		public enum AbnormalType
		{
			[ProtoEnum(Name = "Unknown", Value = 621151)]
			Unknown = 621151,
			[ProtoEnum(Name = "AnotherDeviceLogin", Value = 621152)]
			AnotherDeviceLogin,
			[ProtoEnum(Name = "ServerLogicError", Value = 621153)]
			ServerLogicError,
			[ProtoEnum(Name = "ServerStop", Value = 621154)]
			ServerStop,
			[ProtoEnum(Name = "ClientSendPacketTooQuick", Value = 621156)]
			ClientSendPacketTooQuick = 621156,
			[ProtoEnum(Name = "ServerNotRecPacketTooLong", Value = 621157)]
			ServerNotRecPacketTooLong,
			[ProtoEnum(Name = "ServerCachePacketTooMuch", Value = 621158)]
			ServerCachePacketTooMuch,
			[ProtoEnum(Name = "SecureError", Value = 621159)]
			SecureError,
			[ProtoEnum(Name = "ClientTimeCheckFailure", Value = 621160)]
			ClientTimeCheckFailure,
			[ProtoEnum(Name = "BackendMaintain", Value = 621161)]
			BackendMaintain
		}

		public static readonly short OP = 937;

		private AbnormalDisconnectNty.AbnormalType _abnormalType;

		private string _msg = string.Empty;

		private int _countDownTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "abnormalType", DataFormat = DataFormat.TwosComplement)]
		public AbnormalDisconnectNty.AbnormalType abnormalType
		{
			get
			{
				return this._abnormalType;
			}
			set
			{
				this._abnormalType = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "msg", DataFormat = DataFormat.Default), DefaultValue("")]
		public string msg
		{
			get
			{
				return this._msg;
			}
			set
			{
				this._msg = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "countDownTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int countDownTime
		{
			get
			{
				return this._countDownTime;
			}
			set
			{
				this._countDownTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
