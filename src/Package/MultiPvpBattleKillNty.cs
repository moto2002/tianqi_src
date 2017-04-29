using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(196), ForSend(196), ProtoContract(Name = "MultiPvpBattleKillNty")]
	[Serializable]
	public class MultiPvpBattleKillNty : IExtensible
	{
		public static readonly short OP = 196;

		private string _killerName = string.Empty;

		private string _deadName = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "killerName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string killerName
		{
			get
			{
				return this._killerName;
			}
			set
			{
				this._killerName = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "deadName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string deadName
		{
			get
			{
				return this._deadName;
			}
			set
			{
				this._deadName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
