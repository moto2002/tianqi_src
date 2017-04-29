using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(381), ForSend(381), ProtoContract(Name = "DailyTask")]
	[Serializable]
	public class DailyTask : IExtensible
	{
		public static readonly short OP = 381;

		private int _taskId;

		private int _count;

		private bool _getPrize;

		private long _date;

		private bool _hasPrize;

		private int _canFindTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "taskId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "count", DataFormat = DataFormat.TwosComplement)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "getPrize", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool getPrize
		{
			get
			{
				return this._getPrize;
			}
			set
			{
				this._getPrize = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "date", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long date
		{
			get
			{
				return this._date;
			}
			set
			{
				this._date = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "hasPrize", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hasPrize
		{
			get
			{
				return this._hasPrize;
			}
			set
			{
				this._hasPrize = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "canFindTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int canFindTimes
		{
			get
			{
				return this._canFindTimes;
			}
			set
			{
				this._canFindTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
