using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "WildBossRoleShowInfo")]
	[Serializable]
	public class WildBossRoleShowInfo : IExtensible
	{
		private long _roleId;

		private string _name = string.Empty;

		private int _career;

		private long _hp;

		private long _hpLmt;

		private int _fighting;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "roleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(6, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "hp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this._hp = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "hpLmt", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long hpLmt
		{
			get
			{
				return this._hpLmt;
			}
			set
			{
				this._hpLmt = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "fighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fighting
		{
			get
			{
				return this._fighting;
			}
			set
			{
				this._fighting = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
