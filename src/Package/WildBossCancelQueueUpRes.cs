using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1015), ForSend(1015), ProtoContract(Name = "WildBossCancelQueueUpRes")]
	[Serializable]
	public class WildBossCancelQueueUpRes : IExtensible
	{
		public static readonly short OP = 1015;

		private bool _teamBoss;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "teamBoss", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool teamBoss
		{
			get
			{
				return this._teamBoss;
			}
			set
			{
				this._teamBoss = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
