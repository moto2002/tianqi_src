using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(341), ForSend(341), ProtoContract(Name = "LoginRes")]
	[Serializable]
	public class LoginRes : IExtensible
	{
		public static readonly short OP = 341;

		private RoleInfo _info;

		private ExtraInfo _data;

		private int _createTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "info", DataFormat = DataFormat.Default)]
		public RoleInfo info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "data", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ExtraInfo data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "createTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int createTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				this._createTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
