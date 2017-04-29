using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(714), ForSend(714), ProtoContract(Name = "SettleDungeonRes")]
	[Serializable]
	public class SettleDungeonRes : IExtensible
	{
		public static readonly short OP = 714;

		private ChallengeResult _result;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "result", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ChallengeResult result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
