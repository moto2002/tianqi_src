using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(705), ForSend(705), ProtoContract(Name = "GetServerListRes")]
	[Serializable]
	public class GetServerListRes : IExtensible
	{
		public static readonly short OP = 705;

		private readonly List<SceneServerInfo> _servers = new List<SceneServerInfo>();

		private string _localtime = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "servers", DataFormat = DataFormat.Default)]
		public List<SceneServerInfo> servers
		{
			get
			{
				return this._servers;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "localtime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string localtime
		{
			get
			{
				return this._localtime;
			}
			set
			{
				this._localtime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
