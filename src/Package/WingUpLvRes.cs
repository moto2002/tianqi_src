using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(465), ForSend(465), ProtoContract(Name = "WingUpLvRes")]
	[Serializable]
	public class WingUpLvRes : IExtensible
	{
		public static readonly short OP = 465;

		private WingInfo _wingInfo;

		private int _itemId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "wingInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public WingInfo wingInfo
		{
			get
			{
				return this._wingInfo;
			}
			set
			{
				this._wingInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
