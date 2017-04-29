using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4067), ForSend(4067), ProtoContract(Name = "FindTeamInfoRes")]
	[Serializable]
	public class FindTeamInfoRes : IExtensible
	{
		public static readonly short OP = 4067;

		private int _nPage;

		private readonly List<TeamBaseInfo> _teamInfo = new List<TeamBaseInfo>();

		private TeamDungeonInfo _dungeonInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "nPage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nPage
		{
			get
			{
				return this._nPage;
			}
			set
			{
				this._nPage = value;
			}
		}

		[ProtoMember(2, Name = "teamInfo", DataFormat = DataFormat.Default)]
		public List<TeamBaseInfo> teamInfo
		{
			get
			{
				return this._teamInfo;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "dungeonInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
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
