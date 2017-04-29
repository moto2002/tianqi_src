using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6329), ForSend(6329), ProtoContract(Name = "FriendProtectAnswerReq")]
	[Serializable]
	public class FriendProtectAnswerReq : IExtensible
	{
		public static readonly short OP = 6329;

		private long _inviteRoleId;

		private bool _answerFlag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "inviteRoleId", DataFormat = DataFormat.TwosComplement)]
		public long inviteRoleId
		{
			get
			{
				return this._inviteRoleId;
			}
			set
			{
				this._inviteRoleId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "answerFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool answerFlag
		{
			get
			{
				return this._answerFlag;
			}
			set
			{
				this._answerFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
