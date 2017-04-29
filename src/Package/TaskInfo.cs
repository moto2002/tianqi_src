using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "TaskInfo")]
	[Serializable]
	public class TaskInfo : IExtensible
	{
		private readonly List<Task> _task = new List<Task>();

		private DailyTaskInfo _dailyTaskInfo;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "task", DataFormat = DataFormat.Default)]
		public List<Task> task
		{
			get
			{
				return this._task;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "dailyTaskInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public DailyTaskInfo dailyTaskInfo
		{
			get
			{
				return this._dailyTaskInfo;
			}
			set
			{
				this._dailyTaskInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
