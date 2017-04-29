using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "RankingRoleInfo")]
	[Serializable]
	public class RankingRoleInfo : IExtensible
	{
		private long _roleId;

		private string _name = string.Empty;

		private int _number;

		private long _value;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "number", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int number
		{
			get
			{
				return this._number;
			}
			set
			{
				this._number = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long value
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
