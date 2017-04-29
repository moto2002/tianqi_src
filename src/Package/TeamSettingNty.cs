using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4055), ForSend(4055), ProtoContract(Name = "TeamSettingNty")]
	[Serializable]
	public class TeamSettingNty : IExtensible
	{
		public static readonly short OP = 4055;

		private int _minLv;

		private int _maxLv;

		private string _teamName = string.Empty;

		private int _teamId;

		private long _leaderId;

		private TeamDungeonInfo _dungeonInfo;

		private bool _autoAgreed;

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

		[ProtoMember(4, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "leaderId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long leaderId
		{
			get
			{
				return this._leaderId;
			}
			set
			{
				this._leaderId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "dungeonInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(7, IsRequired = false, Name = "autoAgreed", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool autoAgreed
		{
			get
			{
				return this._autoAgreed;
			}
			set
			{
				this._autoAgreed = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
