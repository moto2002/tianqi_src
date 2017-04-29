using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(129), ForSend(129), ProtoContract(Name = "GuildWarMisNty")]
	[Serializable]
	public class GuildWarMisNty : IExtensible
	{
		public static readonly short OP = 129;

		private bool _championDailyPrize;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "championDailyPrize", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool championDailyPrize
		{
			get
			{
				return this._championDailyPrize;
			}
			set
			{
				this._championDailyPrize = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
