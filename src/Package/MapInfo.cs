using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MapInfo")]
	[Serializable]
	public class MapInfo : IExtensible
	{
		private int _mapId;

		private bool _openFlag;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "mapId", DataFormat = DataFormat.TwosComplement)]
		public int mapId
		{
			get
			{
				return this._mapId;
			}
			set
			{
				this._mapId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "openFlag", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool openFlag
		{
			get
			{
				return this._openFlag;
			}
			set
			{
				this._openFlag = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
