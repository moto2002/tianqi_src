using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7542), ForSend(7542), ProtoContract(Name = "GetStoreInfoRes")]
	[Serializable]
	public class GetStoreInfoRes : IExtensible
	{
		public static readonly short OP = 7542;

		private StoreInfo _storeInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "storeInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public StoreInfo storeInfo
		{
			get
			{
				return this._storeInfo;
			}
			set
			{
				this._storeInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
