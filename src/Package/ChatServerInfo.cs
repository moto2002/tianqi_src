using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(600), ForSend(600), ProtoContract(Name = "ChatServerInfo")]
	[Serializable]
	public class ChatServerInfo : IExtensible
	{
		public static readonly short OP = 600;

		private string _key = string.Empty;

		private string _token = string.Empty;

		private string _ip = string.Empty;

		private int _port;

		private string _ip2 = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "key", DataFormat = DataFormat.Default), DefaultValue("")]
		public string key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "token", DataFormat = DataFormat.Default), DefaultValue("")]
		public string token
		{
			get
			{
				return this._token;
			}
			set
			{
				this._token = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "ip", DataFormat = DataFormat.Default), DefaultValue("")]
		public string ip
		{
			get
			{
				return this._ip;
			}
			set
			{
				this._ip = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "port", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int port
		{
			get
			{
				return this._port;
			}
			set
			{
				this._port = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "ip2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string ip2
		{
			get
			{
				return this._ip2;
			}
			set
			{
				this._ip2 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
