using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "matchRobot")]
	[Serializable]
	public class matchRobot : IExtensible
	{
		[ProtoContract(Name = "EquipmentidPair")]
		[Serializable]
		public class EquipmentidPair : IExtensible
		{
			private int _key;

			private string _value = string.Empty;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
			public string value
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

		private uint _id;

		private readonly List<matchRobot.EquipmentidPair> _equipmentID = new List<matchRobot.EquipmentidPair>();

		private int _enhanceLV;

		private int _petStar;

		private int _petLv;

		private int _petSkillLv;

		private int _gemLV;

		private int _roleSkillLv;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public uint id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, Name = "equipmentID", DataFormat = DataFormat.Default)]
		public List<matchRobot.EquipmentidPair> equipmentID
		{
			get
			{
				return this._equipmentID;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "enhanceLV", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int enhanceLV
		{
			get
			{
				return this._enhanceLV;
			}
			set
			{
				this._enhanceLV = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "petStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petStar
		{
			get
			{
				return this._petStar;
			}
			set
			{
				this._petStar = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "petLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petLv
		{
			get
			{
				return this._petLv;
			}
			set
			{
				this._petLv = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "petSkillLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petSkillLv
		{
			get
			{
				return this._petSkillLv;
			}
			set
			{
				this._petSkillLv = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "gemLV", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gemLV
		{
			get
			{
				return this._gemLV;
			}
			set
			{
				this._gemLV = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "roleSkillLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int roleSkillLv
		{
			get
			{
				return this._roleSkillLv;
			}
			set
			{
				this._roleSkillLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
