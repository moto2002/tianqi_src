using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(512), ForSend(512), ProtoContract(Name = "LoginAuthRes")]
	[Serializable]
	public class LoginAuthRes : IExtensible
	{
		public static readonly short OP = 512;

		private string _account;

		private readonly List<RoleBriefInfo> _roles = new List<RoleBriefInfo>();

		private int _nowServerTime;

		private string _sdkRes = string.Empty;

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

		[ProtoMember(2, Name = "roles", DataFormat = DataFormat.Default)]
		public List<RoleBriefInfo> roles
		{
			get
			{
				return this._roles;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "nowServerTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nowServerTime
		{
			get
			{
				return this._nowServerTime;
			}
			set
			{
				this._nowServerTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "sdkRes", DataFormat = DataFormat.Default), DefaultValue("")]
		public string sdkRes
		{
			get
			{
				return this._sdkRes;
			}
			set
			{
				this._sdkRes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
