using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(726), ForSend(726), ProtoContract(Name = "InviteSettingRes")]
	[Serializable]
	public class InviteSettingRes : IExtensible
	{
		public static readonly short OP = 726;

		private int _roleMinLv;

		private bool _verify;

		private string _notice = string.Empty;

		private bool _available;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "roleMinLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int roleMinLv
		{
			get
			{
				return this._roleMinLv;
			}
			set
			{
				this._roleMinLv = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "verify", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool verify
		{
			get
			{
				return this._verify;
			}
			set
			{
				this._verify = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "notice", DataFormat = DataFormat.Default), DefaultValue("")]
		public string notice
		{
			get
			{
				return this._notice;
			}
			set
			{
				this._notice = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "available", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool available
		{
			get
			{
				return this._available;
			}
			set
			{
				this._available = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
