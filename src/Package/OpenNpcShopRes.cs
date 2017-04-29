using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2675), ForSend(2675), ProtoContract(Name = "OpenNpcShopRes")]
	[Serializable]
	public class OpenNpcShopRes : IExtensible
	{
		public static readonly short OP = 2675;

		private NpcShopInfo _shopInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "shopInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public NpcShopInfo shopInfo
		{
			get
			{
				return this._shopInfo;
			}
			set
			{
				this._shopInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
