using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "HuanJingPanDuanBaiFenBi")]
	[Serializable]
	public class HuanJingPanDuanBaiFenBi : IExtensible
	{
		private int _conditionId;

		private int _Type;

		private string _percentage = string.Empty;

		private int _value;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "conditionId", DataFormat = DataFormat.TwosComplement)]
		public int conditionId
		{
			get
			{
				return this._conditionId;
			}
			set
			{
				this._conditionId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "Type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "percentage", DataFormat = DataFormat.Default), DefaultValue("")]
		public string percentage
		{
			get
			{
				return this._percentage;
			}
			set
			{
				this._percentage = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
