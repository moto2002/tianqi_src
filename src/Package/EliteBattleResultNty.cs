using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(111), ForSend(111), ProtoContract(Name = "EliteBattleResultNty")]
	[Serializable]
	public class EliteBattleResultNty : IExtensible
	{
		public static readonly short OP = 111;

		private ChallengeResult _result;

		private readonly List<ItemBriefInfo> _normalPrize = new List<ItemBriefInfo>();

		private int _remainTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "result", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ChallengeResult result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		[ProtoMember(2, Name = "normalPrize", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> normalPrize
		{
			get
			{
				return this._normalPrize;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "remainTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainTimes
		{
			get
			{
				return this._remainTimes;
			}
			set
			{
				this._remainTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
