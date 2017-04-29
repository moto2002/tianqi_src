using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(547), ForSend(547), ProtoContract(Name = "SetUpGuildReq")]
	[Serializable]
	public class SetUpGuildReq : IExtensible
	{
		public static readonly short OP = 547;

		private string _name;

		private int _roleMinLv;

		private bool _verify = true;

		private string _notice = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "roleMinLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "verify", DataFormat = DataFormat.Default), DefaultValue(true)]
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

		[ProtoMember(4, IsRequired = false, Name = "notice", DataFormat = DataFormat.Default), DefaultValue("")]
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
