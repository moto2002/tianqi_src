using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(135), ForSend(135), ProtoContract(Name = "GuildBattleWarResultNty")]
	[Serializable]
	public class GuildBattleWarResultNty : IExtensible
	{
		public static readonly short OP = 135;

		private long _hp = -1L;

		private bool _battleEnd;

		private string _killerName = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "hp", DataFormat = DataFormat.TwosComplement), DefaultValue(-1L)]
		public long hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this._hp = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "battleEnd", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool battleEnd
		{
			get
			{
				return this._battleEnd;
			}
			set
			{
				this._battleEnd = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "killerName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string killerName
		{
			get
			{
				return this._killerName;
			}
			set
			{
				this._killerName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
