using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3411), ForSend(3411), ProtoContract(Name = "SettleHookNty")]
	[Serializable]
	public class SettleHookNty : IExtensible
	{
		[ProtoContract(Name = "Reason")]
		[Serializable]
		public class Reason : IExtensible
		{
			[ProtoContract(Name = "RS")]
			public enum RS
			{
				[ProtoEnum(Name = "ActiveExit", Value = 1)]
				ActiveExit = 1,
				[ProtoEnum(Name = "SelfDead", Value = 2)]
				SelfDead,
				[ProtoEnum(Name = "TimeOut", Value = 3)]
				TimeOut,
				[ProtoEnum(Name = "Close", Value = 4)]
				Close,
				[ProtoEnum(Name = "Revenge", Value = 5)]
				Revenge,
				[ProtoEnum(Name = "Logout", Value = 6)]
				Logout,
				[ProtoEnum(Name = "UnKnow", Value = 100)]
				UnKnow = 100
			}

			private IExtension extensionObject;

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 3411;

		private SettleHookNty.Reason.RS _settleReason;

		private bool _revengeFlag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "settleReason", DataFormat = DataFormat.TwosComplement)]
		public SettleHookNty.Reason.RS settleReason
		{
			get
			{
				return this._settleReason;
			}
			set
			{
				this._settleReason = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "revengeFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool revengeFlag
		{
			get
			{
				return this._revengeFlag;
			}
			set
			{
				this._revengeFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
