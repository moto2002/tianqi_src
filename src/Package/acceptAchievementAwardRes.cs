using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1086), ForSend(1086), ProtoContract(Name = "acceptAchievementAwardRes")]
	[Serializable]
	public class acceptAchievementAwardRes : IExtensible
	{
		public static readonly short OP = 1086;

		private int _achievementId;

		private AchievementItemInfo _achievementItemInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "achievementId", DataFormat = DataFormat.TwosComplement)]
		public int achievementId
		{
			get
			{
				return this._achievementId;
			}
			set
			{
				this._achievementId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "achievementItemInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public AchievementItemInfo achievementItemInfo
		{
			get
			{
				return this._achievementItemInfo;
			}
			set
			{
				this._achievementItemInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
