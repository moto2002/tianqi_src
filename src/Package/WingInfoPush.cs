using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7712), ForSend(7712), ProtoContract(Name = "WingInfoPush")]
	[Serializable]
	public class WingInfoPush : IExtensible
	{
		public static readonly short OP = 7712;

		private readonly List<WingInfo> _wingInfos = new List<WingInfo>();

		private int _wearWingId;

		private bool _hidden;

		private bool _firstGetWingDetail = true;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "wingInfos", DataFormat = DataFormat.Default)]
		public List<WingInfo> wingInfos
		{
			get
			{
				return this._wingInfos;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "wearWingId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int wearWingId
		{
			get
			{
				return this._wearWingId;
			}
			set
			{
				this._wearWingId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "hidden", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool hidden
		{
			get
			{
				return this._hidden;
			}
			set
			{
				this._hidden = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "firstGetWingDetail", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool firstGetWingDetail
		{
			get
			{
				return this._firstGetWingDetail;
			}
			set
			{
				this._firstGetWingDetail = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
