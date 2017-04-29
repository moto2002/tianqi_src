using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(687), ForSend(687), ProtoContract(Name = "PveExitBattleRes")]
	[Serializable]
	public class PveExitBattleRes : IExtensible
	{
		public static readonly short OP = 687;

		private bool _forceExit;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "forceExit", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool forceExit
		{
			get
			{
				return this._forceExit;
			}
			set
			{
				this._forceExit = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
