using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(510), ForSend(510), ProtoContract(Name = "LoginAuthReq")]
	[Serializable]
	public class LoginAuthReq : IExtensible
	{
		public static readonly short OP = 510;

		private string _account;

		private int _sdk_type;

		private string _param1 = string.Empty;

		private string _param2 = string.Empty;

		private string _param3 = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "account", DataFormat = DataFormat.Default)]
		public string account
		{
			get
			{
				return this._account;
			}
			set
			{
				this._account = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "sdk_type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sdk_type
		{
			get
			{
				return this._sdk_type;
			}
			set
			{
				this._sdk_type = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "param1", DataFormat = DataFormat.Default), DefaultValue("")]
		public string param1
		{
			get
			{
				return this._param1;
			}
			set
			{
				this._param1 = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "param2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string param2
		{
			get
			{
				return this._param2;
			}
			set
			{
				this._param2 = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "param3", DataFormat = DataFormat.Default), DefaultValue("")]
		public string param3
		{
			get
			{
				return this._param3;
			}
			set
			{
				this._param3 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
