using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3903), ForSend(3903), ProtoContract(Name = "GetBossLabelInfoRes")]
	[Serializable]
	public class GetBossLabelInfoRes : IExtensible
	{
		public static readonly short OP = 3903;

		private int _labelId;

		private BossLabelInfo _bossLabelInfo;

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

		[ProtoMember(2, IsRequired = false, Name = "bossLabelInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public BossLabelInfo bossLabelInfo
		{
			get
			{
				return this._bossLabelInfo;
			}
			set
			{
				this._bossLabelInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
