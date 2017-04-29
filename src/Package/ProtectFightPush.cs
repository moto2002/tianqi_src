using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3741), ForSend(3741), ProtoContract(Name = "ProtectFightPush")]
	[Serializable]
	public class ProtectFightPush : IExtensible
	{
		public static readonly short OP = 3741;

		private ProtectFightInfo _info;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "info", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ProtectFightInfo info
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
