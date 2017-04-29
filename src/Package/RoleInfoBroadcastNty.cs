using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(33), ForSend(33), ProtoContract(Name = "RoleInfoBroadcastNty")]
	[Serializable]
	public class RoleInfoBroadcastNty : IExtensible
	{
		public static readonly short OP = 33;

		private int _type;

		private long _roleId;

		private string _oldValue;

		private string _newValue;

		private GuildInfo _guildInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public int type
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

		[ProtoMember(2, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "oldValue", DataFormat = DataFormat.Default)]
		public string oldValue
		{
			get
			{
				return this._oldValue;
			}
			set
			{
				this._oldValue = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "newValue", DataFormat = DataFormat.Default)]
		public string newValue
		{
			get
			{
				return this._newValue;
			}
			set
			{
				this._newValue = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "guildInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public GuildInfo guildInfo
		{
			get
			{
				return this._guildInfo;
			}
			set
			{
				this._guildInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
