using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BlockInfo")]
	[Serializable]
	public class BlockInfo : IExtensible
	{
		private string _blockId;

		private bool _isChallenge;

		private RandomIncidentType.IncidentType _incidentType;

		private int _incidentTypeId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "blockId", DataFormat = DataFormat.Default)]
		public string blockId
		{
			get
			{
				return this._blockId;
			}
			set
			{
				this._blockId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "isChallenge", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isChallenge
		{
			get
			{
				return this._isChallenge;
			}
			set
			{
				this._isChallenge = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "incidentType", DataFormat = DataFormat.TwosComplement), DefaultValue(RandomIncidentType.IncidentType.TOOL)]
		public RandomIncidentType.IncidentType incidentType
		{
			get
			{
				return this._incidentType;
			}
			set
			{
				this._incidentType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "incidentTypeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int incidentTypeId
		{
			get
			{
				return this._incidentTypeId;
			}
			set
			{
				this._incidentTypeId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
