using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4672), ForSend(4672), ProtoContract(Name = "DailyTaskNty")]
	[Serializable]
	public class DailyTaskNty : IExtensible
	{
		public static readonly short OP = 4672;

		private DailyTask _dailyTask;

		private int _totalActivity;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "dailyTask", DataFormat = DataFormat.Default), DefaultValue(null)]
		public DailyTask dailyTask
		{
			get
			{
				return this._dailyTask;
			}
			set
			{
				this._dailyTask = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "totalActivity", DataFormat = DataFormat.TwosComplement)]
		public int totalActivity
		{
			get
			{
				return this._totalActivity;
			}
			set
			{
				this._totalActivity = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
