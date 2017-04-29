using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(676), ForSend(676), ProtoContract(Name = "ExitChallengeReq")]
	[Serializable]
	public class ExitChallengeReq : IExtensible
	{
		public static readonly short OP = 676;

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
