using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7891), ForSend(7891), ProtoContract(Name = "LoginPushTaskInfo")]
	[Serializable]
	public class LoginPushTaskInfo : IExtensible
	{
		public static readonly short OP = 7891;

		private readonly List<Task> _taskInfo = new List<Task>();

		private readonly List<LastTaskId> _lastTaskId = new List<LastTaskId>();

		private bool _guildTaskFlag;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "taskInfo", DataFormat = DataFormat.Default)]
		public List<Task> taskInfo
		{
			get
			{
				return this._taskInfo;
			}
		}

		[ProtoMember(2, Name = "lastTaskId", DataFormat = DataFormat.Default)]
		public List<LastTaskId> lastTaskId
		{
			get
			{
				return this._lastTaskId;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "guildTaskFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool guildTaskFlag
		{
			get
			{
				return this._guildTaskFlag;
			}
			set
			{
				this._guildTaskFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
