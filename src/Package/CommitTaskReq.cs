using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(564), ForSend(564), ProtoContract(Name = "CommitTaskReq")]
	[Serializable]
	public class CommitTaskReq : IExtensible
	{
		public static readonly short OP = 564;

		private int _taskId;

		private bool _endDialogue;

		private bool _playedPreviewMov;

		private bool _playedReviewMov;

		private bool _useDiamond;

		private int _taskNumber;

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

		[ProtoMember(2, IsRequired = false, Name = "endDialogue", DataFormat = DataFormat.Default), DefaultValue(false)]
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

		[ProtoMember(3, IsRequired = false, Name = "playedPreviewMov", DataFormat = DataFormat.Default), DefaultValue(false)]
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

		[ProtoMember(4, IsRequired = false, Name = "playedReviewMov", DataFormat = DataFormat.Default), DefaultValue(false)]
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

		[ProtoMember(5, IsRequired = false, Name = "useDiamond", DataFormat = DataFormat.Default), DefaultValue(false)]
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

		[ProtoMember(6, IsRequired = false, Name = "taskNumber", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskNumber
		{
			get
			{
				return this._taskNumber;
			}
			set
			{
				this._taskNumber = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
