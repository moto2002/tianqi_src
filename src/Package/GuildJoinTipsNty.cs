using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3680), ForSend(3680), ProtoContract(Name = "GuildJoinTipsNty")]
	[Serializable]
	public class GuildJoinTipsNty : IExtensible
	{
		[ProtoContract(Name = "TipsType")]
		public enum TipsType
		{
			[ProtoEnum(Name = "JoinIn", Value = 1)]
			JoinIn = 1,
			[ProtoEnum(Name = "Reject", Value = 2)]
			Reject
		}

		public static readonly short OP = 3680;

		private GuildJoinTipsNty.TipsType _type;

		private string _guildName = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public GuildJoinTipsNty.TipsType type
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

		[ProtoMember(2, IsRequired = false, Name = "guildName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string guildName
		{
			get
			{
				return this._guildName;
			}
			set
			{
				this._guildName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
