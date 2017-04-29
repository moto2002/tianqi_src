using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4066), ForSend(4066), ProtoContract(Name = "FindTeamInfoReq")]
	[Serializable]
	public class FindTeamInfoReq : IExtensible
	{
		public static readonly short OP = 4066;

		private int _nPage = 1;

		private TeamDungeonInfo _dungeonInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "nPage", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
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
