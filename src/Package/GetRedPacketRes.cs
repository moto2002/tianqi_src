using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(180), ForSend(180), ProtoContract(Name = "GetRedPacketRes")]
	[Serializable]
	public class GetRedPacketRes : IExtensible
	{
		[ProtoContract(Name = "RedPacketType")]
		public enum RedPacketType
		{
			[ProtoEnum(Name = "Recharges", Value = 1)]
			Recharges = 1,
			[ProtoEnum(Name = "KillBoss", Value = 2)]
			KillBoss,
			[ProtoEnum(Name = "VipLv", Value = 3)]
			VipLv,
			[ProtoEnum(Name = "TotalVipLv", Value = 4)]
			TotalVipLv,
			[ProtoEnum(Name = "TotalRechargeTimes", Value = 5)]
			TotalRechargeTimes,
			[ProtoEnum(Name = "TotalTwistedEggTimes", Value = 6)]
			TotalTwistedEggTimes
		}

		public static readonly short OP = 180;

		private int _id;

		private int _taskId;

		private bool _Status;

		private GetRedPacketRes.RedPacketType _Type = GetRedPacketRes.RedPacketType.Recharges;

		private readonly List<ItemBriefInfo> _rewards = new List<ItemBriefInfo>();

		private readonly List<string> _parameter = new List<string>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "taskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskId
		{
			get
			{
				return this._taskId;
			}
			set
			{
				this._taskId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "Status", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Type", DataFormat = DataFormat.TwosComplement), DefaultValue(GetRedPacketRes.RedPacketType.Recharges)]
		public GetRedPacketRes.RedPacketType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}

		[ProtoMember(5, Name = "rewards", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> rewards
		{
			get
			{
				return this._rewards;
			}
		}

		[ProtoMember(6, Name = "parameter", DataFormat = DataFormat.Default)]
		public List<string> parameter
		{
			get
			{
				return this._parameter;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
