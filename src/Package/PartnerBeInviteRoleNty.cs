using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4062), ForSend(4062), ProtoContract(Name = "PartnerBeInviteRoleNty")]
	[Serializable]
	public class PartnerBeInviteRoleNty : IExtensible
	{
		public static readonly short OP = 4062;

		private MemberResume _inviteResume;

		private int _teamId;

		private string _teamName = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "inviteResume", DataFormat = DataFormat.Default)]
		public MemberResume inviteResume
		{
			get
			{
				return this._inviteResume;
			}
			set
			{
				this._inviteResume = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int teamId
		{
			get
			{
				return this._teamId;
			}
			set
			{
				this._teamId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "teamName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string teamName
		{
			get
			{
				return this._teamName;
			}
			set
			{
				this._teamName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
