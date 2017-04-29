using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4012), ForSend(4012), ProtoContract(Name = "BuildTeamRes")]
	[Serializable]
	public class BuildTeamRes : IExtensible
	{
		public static readonly short OP = 4012;

		private TeamType.ENUM _teamType;

		private ulong _teamId;

		private int _dungeonId;

		private string _dungeonName = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "teamType", DataFormat = DataFormat.TwosComplement)]
		public TeamType.ENUM teamType
		{
			get
			{
				return this._teamType;
			}
			set
			{
				this._teamType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0f)]
		public ulong teamId
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

		[ProtoMember(3, IsRequired = false, Name = "dungeonId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dungeonId
		{
			get
			{
				return this._dungeonId;
			}
			set
			{
				this._dungeonId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "dungeonName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string dungeonName
		{
			get
			{
				return this._dungeonName;
			}
			set
			{
				this._dungeonName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
