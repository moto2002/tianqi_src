using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(840), ForSend(840), ProtoContract(Name = "EquipSuitChangeNty")]
	[Serializable]
	public class EquipSuitChangeNty : IExtensible
	{
		public static readonly short OP = 840;

		private readonly List<SuitInfo> _suitInfos = new List<SuitInfo>();

		private SuitActiveIndexId _ids;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "suitInfos", DataFormat = DataFormat.Default)]
		public List<SuitInfo> suitInfos
		{
			get
			{
				return this._suitInfos;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "ids", DataFormat = DataFormat.Default), DefaultValue(null)]
		public SuitActiveIndexId ids
		{
			get
			{
				return this._ids;
			}
			set
			{
				this._ids = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
