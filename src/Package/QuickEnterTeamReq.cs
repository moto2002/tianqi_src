using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2313), ForSend(2313), ProtoContract(Name = "QuickEnterTeamReq")]
	[Serializable]
	public class QuickEnterTeamReq : IExtensible
	{
		public static readonly short OP = 2313;

		private TeamDungeonInfo _dungeonInfo;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
