using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(5731), ForSend(5731), ProtoContract(Name = "GetExtraRewardReq")]
	[Serializable]
	public class GetExtraRewardReq : IExtensible
	{
		public static readonly short OP = 5731;

		private int _taskType;

		private int _timePoint;

		private bool _useDiamond;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "taskType", DataFormat = DataFormat.TwosComplement)]
		public int taskType
		{
			get
			{
				return this._taskType;
			}
			set
			{
				this._taskType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "timePoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int timePoint
		{
			get
			{
				return this._timePoint;
			}
			set
			{
				this._timePoint = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "useDiamond", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool useDiamond
		{
			get
			{
				return this._useDiamond;
			}
			set
			{
				this._useDiamond = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
