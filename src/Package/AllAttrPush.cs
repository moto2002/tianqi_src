using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3434), ForSend(3434), ProtoContract(Name = "AllAttrPush")]
	[Serializable]
	public class AllAttrPush : IExtensible
	{
		public static readonly short OP = 3434;

		private MapObjInfo _info;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "info", DataFormat = DataFormat.Default), DefaultValue(null)]
		public MapObjInfo info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
