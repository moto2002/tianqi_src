using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(606), ForSend(606), ProtoContract(Name = "WildBossInfoNty")]
	[Serializable]
	public class WildBossInfoNty : IExtensible
	{
		public static readonly short OP = 606;

		private bool _rewardLmt;

		private readonly List<WildBossInfo> _bossInfo = new List<WildBossInfo>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "rewardLmt", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool rewardLmt
		{
			get
			{
				return this._rewardLmt;
			}
			set
			{
				this._rewardLmt = value;
			}
		}

		[ProtoMember(1, Name = "bossInfo", DataFormat = DataFormat.Default)]
		public List<WildBossInfo> bossInfo
		{
			get
			{
				return this._bossInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
