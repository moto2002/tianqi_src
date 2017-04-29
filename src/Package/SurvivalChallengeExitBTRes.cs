using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1113), ForSend(1113), ProtoContract(Name = "SurvivalChallengeExitBTRes")]
	[Serializable]
	public class SurvivalChallengeExitBTRes : IExtensible
	{
		public static readonly short OP = 1113;

		private bool _again;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "again", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool again
		{
			get
			{
				return this._again;
			}
			set
			{
				this._again = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
