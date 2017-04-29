using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7217), ForSend(7217), ProtoContract(Name = "QuickEnterTeamRes")]
	[Serializable]
	public class QuickEnterTeamRes : IExtensible
	{
		public static readonly short OP = 7217;

		private TeamDungeonInfo _dungeonInfo;

		private readonly List<TeamJoinCD> _teamJoinCd = new List<TeamJoinCD>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "dungeonInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(2, Name = "teamJoinCd", DataFormat = DataFormat.Default)]
		public List<TeamJoinCD> teamJoinCd
		{
			get
			{
				return this._teamJoinCd;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
