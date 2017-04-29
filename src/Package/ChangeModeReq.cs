using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3455), ForSend(3455), ProtoContract(Name = "ChangeModeReq")]
	[Serializable]
	public class ChangeModeReq : IExtensible
	{
		public static readonly short OP = 3455;

		private bool _isPk;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "isPk", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isPk
		{
			get
			{
				return this._isPk;
			}
			set
			{
				this._isPk = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
