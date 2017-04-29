using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DBActiveCenterInfo")]
	[Serializable]
	public class DBActiveCenterInfo : IExtensible
	{
		private int _id;

		private int _remainTimes;

		private bool _runFlag;

		private int _runStatus;

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

		[ProtoMember(2, IsRequired = false, Name = "remainTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "runFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool runFlag
		{
			get
			{
				return this._runFlag;
			}
			set
			{
				this._runFlag = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "runStatus", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int runStatus
		{
			get
			{
				return this._runStatus;
			}
			set
			{
				this._runStatus = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
