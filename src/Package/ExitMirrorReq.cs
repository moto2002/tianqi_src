using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(582), ForSend(582), ProtoContract(Name = "ExitMirrorReq")]
	[Serializable]
	public class ExitMirrorReq : IExtensible
	{
		public static readonly short OP = 582;

		private Pos _pos;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "pos", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Pos pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
