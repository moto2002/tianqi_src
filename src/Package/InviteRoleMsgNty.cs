using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(711), ForSend(711), ProtoContract(Name = "InviteRoleMsgNty")]
	[Serializable]
	public class InviteRoleMsgNty : IExtensible
	{
		public static readonly short OP = 711;

		private MemberResume _inviteResume;

		private TeamType.ENUM _teamType;

		private ulong _teamId;

		private int _sectionId;

		private string _sectionName = string.Empty;

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

		[ProtoMember(2, IsRequired = true, Name = "teamType", DataFormat = DataFormat.TwosComplement)]
		public TeamType.ENUM teamType
		{
			get
			{
				return this._teamType;
			}
			set
			{
				this._teamType = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0f)]
		public ulong teamId
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

		[ProtoMember(3, IsRequired = false, Name = "sectionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sectionId
		{
			get
			{
				return this._sectionId;
			}
			set
			{
				this._sectionId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "sectionName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string sectionName
		{
			get
			{
				return this._sectionName;
			}
			set
			{
				this._sectionName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
