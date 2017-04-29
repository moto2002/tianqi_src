using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3669), ForSend(3669), ProtoContract(Name = "GuildInfoNty")]
	[Serializable]
	public class GuildInfoNty : IExtensible
	{
		public static readonly short OP = 3669;

		private GuildBaseInfo _baseInfo;

		private MemberInfo _memberInfo;

		private bool _hidden;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "baseInfo", DataFormat = DataFormat.Default)]
		public GuildBaseInfo baseInfo
		{
			get
			{
				return this._baseInfo;
			}
			set
			{
				this._baseInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "memberInfo", DataFormat = DataFormat.Default)]
		public MemberInfo memberInfo
		{
			get
			{
				return this._memberInfo;
			}
			set
			{
				this._memberInfo = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "hidden", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hidden
		{
			get
			{
				return this._hidden;
			}
			set
			{
				this._hidden = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
