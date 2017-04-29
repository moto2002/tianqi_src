using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GlobalMail")]
	[Serializable]
	public class GlobalMail : IExtensible
	{
		private MailSendType.MST _type;

		private readonly List<long> _roleIds = new List<long>();

		private MailPreset _mail;

		private MailRecvCond _recvCond;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public MailSendType.MST type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, Name = "roleIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> roleIds
		{
			get
			{
				return this._roleIds;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "mail", DataFormat = DataFormat.Default)]
		public MailPreset mail
		{
			get
			{
				return this._mail;
			}
			set
			{
				this._mail = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "recvCond", DataFormat = DataFormat.Default), DefaultValue(null)]
		public MailRecvCond recvCond
		{
			get
			{
				return this._recvCond;
			}
			set
			{
				this._recvCond = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
