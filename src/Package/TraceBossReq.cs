using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4485), ForSend(4485), ProtoContract(Name = "TraceBossReq")]
	[Serializable]
	public class TraceBossReq : IExtensible
	{
		public static readonly short OP = 4485;

		private int _labelId;

		private bool _opFlag = true;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "labelId", DataFormat = DataFormat.TwosComplement)]
		public int labelId
		{
			get
			{
				return this._labelId;
			}
			set
			{
				this._labelId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "opFlag", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool opFlag
		{
			get
			{
				return this._opFlag;
			}
			set
			{
				this._opFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
