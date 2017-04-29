using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2183), ForSend(2183), ProtoContract(Name = "GetGuildWarChampionRes")]
	[Serializable]
	public class GetGuildWarChampionRes : IExtensible
	{
		public static readonly short OP = 2183;

		private string _guildName = string.Empty;

		private int _winnerTimes;

		private string _captainName = string.Empty;

		private int _career;

		private string _titleName = string.Empty;

		private int _titleCD;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "guildName", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(2, IsRequired = false, Name = "winnerTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int winnerTimes
		{
			get
			{
				return this._winnerTimes;
			}
			set
			{
				this._winnerTimes = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "captainName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string captainName
		{
			get
			{
				return this._captainName;
			}
			set
			{
				this._captainName = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "titleName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string titleName
		{
			get
			{
				return this._titleName;
			}
			set
			{
				this._titleName = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "titleCD", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int titleCD
		{
			get
			{
				return this._titleCD;
			}
			set
			{
				this._titleCD = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
