using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(766), ForSend(766), ProtoContract(Name = "FightingChangedNty")]
	[Serializable]
	public class FightingChangedNty : IExtensible
	{
		[ProtoContract(Name = "FightingType")]
		public enum FightingType
		{
			[ProtoEnum(Name = "Panel", Value = 0)]
			Panel,
			[ProtoEnum(Name = "RoleAttr", Value = 1)]
			RoleAttr,
			[ProtoEnum(Name = "RoleSkill", Value = 2)]
			RoleSkill,
			[ProtoEnum(Name = "RoleTalent", Value = 3)]
			RoleTalent,
			[ProtoEnum(Name = "PetInBattle", Value = 4)]
			PetInBattle
		}

		public static readonly short OP = 766;

		private int _oldFighting;

		private int _newFighting;

		private FightingChangedNty.FightingType _fightingType;

		private bool _asTips;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "oldFighting", DataFormat = DataFormat.TwosComplement)]
		public int oldFighting
		{
			get
			{
				return this._oldFighting;
			}
			set
			{
				this._oldFighting = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "newFighting", DataFormat = DataFormat.TwosComplement)]
		public int newFighting
		{
			get
			{
				return this._newFighting;
			}
			set
			{
				this._newFighting = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "fightingType", DataFormat = DataFormat.TwosComplement)]
		public FightingChangedNty.FightingType fightingType
		{
			get
			{
				return this._fightingType;
			}
			set
			{
				this._fightingType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "asTips", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool asTips
		{
			get
			{
				return this._asTips;
			}
			set
			{
				this._asTips = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
