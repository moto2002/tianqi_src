using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1337), ForSend(1337), ProtoContract(Name = "ChatLoginReq")]
	[Serializable]
	public class ChatLoginReq : IExtensible
	{
		public static readonly short OP = 1337;

		private long _roleId;

		private string _key = string.Empty;

		private string _token = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "key", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(3, IsRequired = false, Name = "token", DataFormat = DataFormat.Default), DefaultValue("")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
