using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4051), ForSend(4051), ProtoContract(Name = "OrganizeTeamReq")]
	[Serializable]
	public class OrganizeTeamReq : IExtensible
	{
		public static readonly short OP = 4051;

		private int _systemId;

		private TeamDungeonInfo _dungeonInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "systemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int systemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				this._systemId = value;
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
