using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "SceneServerInfo")]
	[Serializable]
	public class SceneServerInfo : IExtensible
	{
		[ProtoContract(Name = "ServerStatusType")]
		public enum ServerStatusType
		{
			[ProtoEnum(Name = "Busy", Value = 1)]
			Busy = 1,
			[ProtoEnum(Name = "Smoothly", Value = 2)]
			Smoothly
		}

		private int _serverId;

		private string _serverName = "server";

		private string _host;

		private int _port;

		private SceneServerInfo.ServerStatusType _status;

		private string _host2 = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "serverId", DataFormat = DataFormat.TwosComplement)]
		public int serverId
		{
			get
			{
				return this._serverId;
			}
			set
			{
				this._serverId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "serverName", DataFormat = DataFormat.Default), DefaultValue("server")]
		public string serverName
		{
			get
			{
				return this._serverName;
			}
			set
			{
				this._serverName = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "host", DataFormat = DataFormat.Default)]
		public string host
		{
			get
			{
				return this._host;
			}
			set
			{
				this._host = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "port", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = true, Name = "status", DataFormat = DataFormat.TwosComplement)]
		public SceneServerInfo.ServerStatusType status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "host2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string host2
		{
			get
			{
				return this._host2;
			}
			set
			{
				this._host2 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
