using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(172), ForSend(172), ProtoContract(Name = "Task")]
	[Serializable]
	public class Task : IExtensible
	{
		[ProtoContract(Name = "TaskStatus")]
		public enum TaskStatus
		{
			[ProtoEnum(Name = "TaskNotOpen", Value = 1)]
			TaskNotOpen = 1,
			[ProtoEnum(Name = "TaskCanAccept", Value = 2)]
			TaskCanAccept,
			[ProtoEnum(Name = "TaskReceived", Value = 3)]
			TaskReceived,
			[ProtoEnum(Name = "WaitingToClaimPrize", Value = 4)]
			WaitingToClaimPrize,
			[ProtoEnum(Name = "TaskFinished", Value = 5)]
			TaskFinished
		}

		[ProtoContract(Name = "TaskType")]
		public enum TaskType
		{
			[ProtoEnum(Name = "MainTask", Value = 1)]
			MainTask = 1,
			[ProtoEnum(Name = "BranchTask", Value = 2)]
			BranchTask,
			[ProtoEnum(Name = "RingTask", Value = 3)]
			RingTask,
			[ProtoEnum(Name = "GuildTask", Value = 4)]
			GuildTask,
			[ProtoEnum(Name = "RingTask2", Value = 5)]
			RingTask2,
			[ProtoEnum(Name = "ChangeCareer", Value = 6)]
			ChangeCareer,
			[ProtoEnum(Name = "GodWeaponTask", Value = 7)]
			GodWeaponTask,
			[ProtoEnum(Name = "ZeroCity", Value = 8)]
			ZeroCity,
			[ProtoEnum(Name = "AdvancedTask", Value = 9)]
			AdvancedTask
		}

		[ProtoContract(Name = "TaskNumber")]
		public enum TaskNumber
		{
			[ProtoEnum(Name = "One", Value = 1)]
			One = 1,
			[ProtoEnum(Name = "Two", Value = 2)]
			Two,
			[ProtoEnum(Name = "Three", Value = 3)]
			Three,
			[ProtoEnum(Name = "Four", Value = 4)]
			Four
		}

		public static readonly short OP = 172;

		private int _taskId;

		private int _count;

		private Task.TaskStatus _status = Task.TaskStatus.TaskNotOpen;

		private bool _playedPreviewMov;

		private bool _playedReviewMov;

		private bool _endDialogue;

		private Task.TaskType _taskType = Task.TaskType.MainTask;

		private int _ratio;

		private readonly List<DropItem> _dropItem = new List<DropItem>();

		private readonly List<int> _extParams = new List<int>();

		private readonly List<DropItem> _groupDropItem = new List<DropItem>();

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

		[ProtoMember(3, IsRequired = false, Name = "status", DataFormat = DataFormat.TwosComplement), DefaultValue(Task.TaskStatus.TaskNotOpen)]
		public Task.TaskStatus status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "playedPreviewMov", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool playedPreviewMov
		{
			get
			{
				return this._playedPreviewMov;
			}
			set
			{
				this._playedPreviewMov = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "playedReviewMov", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool playedReviewMov
		{
			get
			{
				return this._playedReviewMov;
			}
			set
			{
				this._playedReviewMov = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "endDialogue", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool endDialogue
		{
			get
			{
				return this._endDialogue;
			}
			set
			{
				this._endDialogue = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "taskType", DataFormat = DataFormat.TwosComplement), DefaultValue(Task.TaskType.MainTask)]
		public Task.TaskType taskType
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

		[ProtoMember(8, IsRequired = false, Name = "ratio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ratio
		{
			get
			{
				return this._ratio;
			}
			set
			{
				this._ratio = value;
			}
		}

		[ProtoMember(9, Name = "dropItem", DataFormat = DataFormat.Default)]
		public List<DropItem> dropItem
		{
			get
			{
				return this._dropItem;
			}
		}

		[ProtoMember(10, Name = "extParams", DataFormat = DataFormat.TwosComplement)]
		public List<int> extParams
		{
			get
			{
				return this._extParams;
			}
		}

		[ProtoMember(11, Name = "groupDropItem", DataFormat = DataFormat.Default)]
		public List<DropItem> groupDropItem
		{
			get
			{
				return this._groupDropItem;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
