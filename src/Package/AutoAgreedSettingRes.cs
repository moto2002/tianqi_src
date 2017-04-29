using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6372), ForSend(6372), ProtoContract(Name = "AutoAgreedSettingRes")]
	[Serializable]
	public class AutoAgreedSettingRes : IExtensible
	{
		public static readonly short OP = 6372;

		private bool _autoAgreed;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "autoAgreed", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool autoAgreed
		{
			get
			{
				return this._autoAgreed;
			}
			set
			{
				this._autoAgreed = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
