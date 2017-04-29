using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DBSkillTrainInfo")]
	[Serializable]
	public class DBSkillTrainInfo : IExtensible
	{
		private int _number;

		private int _skillLv;

		private bool _isEquip1;

		private bool _isEquip2;

		private bool _isEquip3;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "number", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "skillLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillLv
		{
			get
			{
				return this._skillLv;
			}
			set
			{
				this._skillLv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "isEquip1", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isEquip1
		{
			get
			{
				return this._isEquip1;
			}
			set
			{
				this._isEquip1 = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "isEquip2", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isEquip2
		{
			get
			{
				return this._isEquip2;
			}
			set
			{
				this._isEquip2 = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "isEquip3", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isEquip3
		{
			get
			{
				return this._isEquip3;
			}
			set
			{
				this._isEquip3 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
