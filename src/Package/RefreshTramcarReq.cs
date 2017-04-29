using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2982), ForSend(2982), ProtoContract(Name = "RefreshTramcarReq")]
	[Serializable]
	public class RefreshTramcarReq : IExtensible
	{
		public static readonly short OP = 2982;

		private int _targetQuality;

		private bool _useDiamond;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "targetQuality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetQuality
		{
			get
			{
				return this._targetQuality;
			}
			set
			{
				this._targetQuality = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "useDiamond", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool useDiamond
		{
			get
			{
				return this._useDiamond;
			}
			set
			{
				this._useDiamond = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
