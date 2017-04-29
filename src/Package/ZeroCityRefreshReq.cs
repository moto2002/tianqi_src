using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(5732), ForSend(5732), ProtoContract(Name = "ZeroCityRefreshReq")]
	[Serializable]
	public class ZeroCityRefreshReq : IExtensible
	{
		public static readonly short OP = 5732;

		private bool _free = true;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "free", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool free
		{
			get
			{
				return this._free;
			}
			set
			{
				this._free = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
