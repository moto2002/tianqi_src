using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "EliteCopyInfo")]
	[Serializable]
	public class EliteCopyInfo : IExtensible
	{
		private int _mapId;

		private int _copyId;

		private bool _clearance;

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

		[ProtoMember(2, IsRequired = true, Name = "copyId", DataFormat = DataFormat.TwosComplement)]
		public int copyId
		{
			get
			{
				return this._copyId;
			}
			set
			{
				this._copyId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "clearance", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool clearance
		{
			get
			{
				return this._clearance;
			}
			set
			{
				this._clearance = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
