using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7763), ForSend(7763), ProtoContract(Name = "WingComposeRes")]
	[Serializable]
	public class WingComposeRes : IExtensible
	{
		public static readonly short OP = 7763;

		private WingInfo _wingInfo;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
