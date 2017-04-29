using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4053), ForSend(4053), ProtoContract(Name = "TeamSettingReq")]
	[Serializable]
	public class TeamSettingReq : IExtensible
	{
		public static readonly short OP = 4053;

		private int _minLv;

		private int _maxLv;

		private string _teamName = string.Empty;

		private TeamDungeonInfo _dungeonInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "maxLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxLv
		{
			get
			{
				return this._maxLv;
			}
			set
			{
				this._maxLv = value;
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

		[ProtoMember(4, IsRequired = false, Name = "dungeonInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public TeamDungeonInfo dungeonInfo
		{
			get
			{
				return this._dungeonInfo;
			}
			set
			{
				this._dungeonInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
