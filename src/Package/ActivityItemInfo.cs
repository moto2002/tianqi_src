using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ActivityItemInfo")]
	[Serializable]
	public class ActivityItemInfo : IExtensible
	{
		[ProtoContract(Name = "ActivityStep")]
		[Serializable]
		public class ActivityStep : IExtensible
		{
			[ProtoContract(Name = "ACT")]
			public enum ACT
			{
				[ProtoEnum(Name = "Ready", Value = 1)]
				Ready = 1,
				[ProtoEnum(Name = "Start", Value = 2)]
				Start,
				[ProtoEnum(Name = "Close", Value = 3)]
				Close
			}

			private IExtension extensionObject;

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _typeId;

		private int _activityId;

		private bool _canGetFlag;

		private ActivityItemInfo.ActivityStep.ACT _status;

		private readonly List<int> _timeout = new List<int>();

		private readonly List<int> _progress = new List<int>();

		private bool _hasGetPrize;

		private RawInfo _rawInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "activityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activityId
		{
			get
			{
				return this._activityId;
			}
			set
			{
				this._activityId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "canGetFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool canGetFlag
		{
			get
			{
				return this._canGetFlag;
			}
			set
			{
				this._canGetFlag = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "status", DataFormat = DataFormat.TwosComplement)]
		public ActivityItemInfo.ActivityStep.ACT status
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

		[ProtoMember(5, Name = "timeout", DataFormat = DataFormat.TwosComplement)]
		public List<int> timeout
		{
			get
			{
				return this._timeout;
			}
		}

		[ProtoMember(6, Name = "progress", DataFormat = DataFormat.TwosComplement)]
		public List<int> progress
		{
			get
			{
				return this._progress;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "hasGetPrize", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hasGetPrize
		{
			get
			{
				return this._hasGetPrize;
			}
			set
			{
				this._hasGetPrize = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "rawInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public RawInfo rawInfo
		{
			get
			{
				return this._rawInfo;
			}
			set
			{
				this._rawInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
