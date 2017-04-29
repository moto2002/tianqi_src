using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4082), ForSend(4082), ProtoContract(Name = "JoinTeamNty")]
	[Serializable]
	public class JoinTeamNty : IExtensible
	{
		public static readonly short OP = 4082;

		private TeamBaseInfo _teamInfo;

		private TeamDungeonInfo _dungeonInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "teamInfo", DataFormat = DataFormat.Default)]
		public TeamBaseInfo teamInfo
		{
			get
			{
				return this._teamInfo;
			}
			set
			{
				this._teamInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "dungeonInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
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
