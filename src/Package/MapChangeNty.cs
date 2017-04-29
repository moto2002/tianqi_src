using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2719), ForSend(2719), ProtoContract(Name = "MapChangeNty")]
	[Serializable]
	public class MapChangeNty : IExtensible
	{
		public static readonly short OP = 2719;

		private MapInfo _info;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "info", DataFormat = DataFormat.Default), DefaultValue(null)]
		public MapInfo info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
