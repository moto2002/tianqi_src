using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(724), ForSend(724), ProtoContract(Name = "InviteSettingReq")]
	[Serializable]
	public class InviteSettingReq : IExtensible
	{
		public static readonly short OP = 724;

		private int _roleMinLv;

		private bool _verify;

		private string _notice = string.Empty;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
