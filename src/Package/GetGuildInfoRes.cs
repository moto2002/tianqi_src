using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(618), ForSend(618), ProtoContract(Name = "GetGuildInfoRes")]
	[Serializable]
	public class GetGuildInfoRes : IExtensible
	{
		public static readonly short OP = 618;

		private GuildBaseInfo _baseInfo;

		private readonly List<MemberInfo> _members = new List<MemberInfo>();

		private InviteSetting _setting;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "baseInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(2, Name = "members", DataFormat = DataFormat.Default)]
		public List<MemberInfo> members
		{
			get
			{
				return this._members;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "setting", DataFormat = DataFormat.Default), DefaultValue(null)]
		public InviteSetting setting
		{
			get
			{
				return this._setting;
			}
			set
			{
				this._setting = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
