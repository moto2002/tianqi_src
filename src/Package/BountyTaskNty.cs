using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(782), ForSend(782), ProtoContract(Name = "BountyTaskNty")]
	[Serializable]
	public class BountyTaskNty : IExtensible
	{
		public static readonly short OP = 782;

		private BountyTaskType.ENUM _taskType;

		private int _taskId;

		private bool _bOpen;

		private int _value;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "taskType", DataFormat = DataFormat.TwosComplement)]
		public BountyTaskType.ENUM taskType
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

		[ProtoMember(2, IsRequired = true, Name = "taskId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "bOpen", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool bOpen
		{
			get
			{
				return this._bOpen;
			}
			set
			{
				this._bOpen = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
