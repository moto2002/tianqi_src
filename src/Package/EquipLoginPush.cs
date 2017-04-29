using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(627), ForSend(627), ProtoContract(Name = "EquipLoginPush")]
	[Serializable]
	public class EquipLoginPush : IExtensible
	{
		public static readonly short OP = 627;

		private readonly List<EquipLib> _equipLibs = new List<EquipLib>();

		private readonly List<SuitInfo> _suitInfos = new List<SuitInfo>();

		private SuitActiveIndexId _ids;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "equipLibs", DataFormat = DataFormat.Default)]
		public List<EquipLib> equipLibs
		{
			get
			{
				return this._equipLibs;
			}
		}

		[ProtoMember(2, Name = "suitInfos", DataFormat = DataFormat.Default)]
		public List<SuitInfo> suitInfos
		{
			get
			{
				return this._suitInfos;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "ids", DataFormat = DataFormat.Default), DefaultValue(null)]
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
