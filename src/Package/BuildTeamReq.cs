using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4011), ForSend(4011), ProtoContract(Name = "BuildTeamReq")]
	[Serializable]
	public class BuildTeamReq : IExtensible
	{
		public static readonly short OP = 4011;

		private TeamType.ENUM _teamType;

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

		[ProtoMember(2, IsRequired = false, Name = "dungeonId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "dungeonName", DataFormat = DataFormat.Default), DefaultValue("")]
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
