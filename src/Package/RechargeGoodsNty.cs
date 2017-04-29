using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(144), ForSend(144), ProtoContract(Name = "RechargeGoodsNty")]
	[Serializable]
	public class RechargeGoodsNty : IExtensible
	{
		public static readonly short OP = 144;

		private bool _update;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "update", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool update
		{
			get
			{
				return this._update;
			}
			set
			{
				this._update = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
