using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(675), ForSend(675), ProtoContract(Name = "EquipAdvancedRes")]
	[Serializable]
	public class EquipAdvancedRes : IExtensible
	{
		public static readonly short OP = 675;

		private int _position;

		private EquipSimpleInfo _equip;

		private readonly List<long> _sourceEquipIds = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "equip", DataFormat = DataFormat.Default), DefaultValue(null)]
		public EquipSimpleInfo equip
		{
			get
			{
				return this._equip;
			}
			set
			{
				this._equip = value;
			}
		}

		[ProtoMember(3, Name = "sourceEquipIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> sourceEquipIds
		{
			get
			{
				return this._sourceEquipIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
