using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6689), ForSend(6689), ProtoContract(Name = "BossLabelInfoNty")]
	[Serializable]
	public class BossLabelInfoNty : IExtensible
	{
		public static readonly short OP = 6689;

		private readonly List<int> _labelId = new List<int>();

		private bool _isTraceFlag;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "labelId", DataFormat = DataFormat.TwosComplement)]
		public List<int> labelId
		{
			get
			{
				return this._labelId;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "isTraceFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isTraceFlag
		{
			get
			{
				return this._isTraceFlag;
			}
			set
			{
				this._isTraceFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
