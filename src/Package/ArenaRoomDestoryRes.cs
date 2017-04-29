using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(868), ForSend(868), ProtoContract(Name = "ArenaRoomDestoryRes")]
	[Serializable]
	public class ArenaRoomDestoryRes : IExtensible
	{
		public static readonly short OP = 868;

		private bool _isInitiative = true;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "isInitiative", DataFormat = DataFormat.Default), DefaultValue(true)]
		public bool isInitiative
		{
			get
			{
				return this._isInitiative;
			}
			set
			{
				this._isInitiative = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
