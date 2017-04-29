using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "robot")]
	[Serializable]
	public class robot : IExtensible
	{
		[ProtoContract(Name = "RoleskillPair")]
		[Serializable]
		public class RoleskillPair : IExtensible
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

		private uint _modelId;

		private uint _id;

		private int _equipmentStep;

		private int _equipQuality;

		private readonly List<int> _petID = new List<int>();

		private int _petQuality;

		private int _petLv;

		private readonly List<robot.RoleskillPair> _roleSkill = new List<robot.RoleskillPair>();

		private int _roleSkillLv;

		private long _hp;

		private int _atk;

		private int _defence;

		private int _hit;

		private int _dex;

		private int _crt;

		private int _critHurtAddRatio;

		private int _parry;

		private int _vigour;

		private int _parryHurtDeRatio;

		private int _fighting;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "modelId", DataFormat = DataFormat.TwosComplement)]
		public uint modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "equipmentStep", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equipmentStep
		{
			get
			{
				return this._equipmentStep;
			}
			set
			{
				this._equipmentStep = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "equipQuality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equipQuality
		{
			get
			{
				return this._equipQuality;
			}
			set
			{
				this._equipQuality = value;
			}
		}

		[ProtoMember(6, Name = "petID", DataFormat = DataFormat.TwosComplement)]
		public List<int> petID
		{
			get
			{
				return this._petID;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "petQuality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petQuality
		{
			get
			{
				return this._petQuality;
			}
			set
			{
				this._petQuality = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "petLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, Name = "roleSkill", DataFormat = DataFormat.Default)]
		public List<robot.RoleskillPair> roleSkill
		{
			get
			{
				return this._roleSkill;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "roleSkillLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "hp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(12, IsRequired = false, Name = "atk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int atk
		{
			get
			{
				return this._atk;
			}
			set
			{
				this._atk = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "defence", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int defence
		{
			get
			{
				return this._defence;
			}
			set
			{
				this._defence = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "hit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hit
		{
			get
			{
				return this._hit;
			}
			set
			{
				this._hit = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "dex", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dex
		{
			get
			{
				return this._dex;
			}
			set
			{
				this._dex = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "crt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int crt
		{
			get
			{
				return this._crt;
			}
			set
			{
				this._crt = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "critHurtAddRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int critHurtAddRatio
		{
			get
			{
				return this._critHurtAddRatio;
			}
			set
			{
				this._critHurtAddRatio = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "parry", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int parry
		{
			get
			{
				return this._parry;
			}
			set
			{
				this._parry = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "vigour", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vigour
		{
			get
			{
				return this._vigour;
			}
			set
			{
				this._vigour = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "parryHurtDeRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int parryHurtDeRatio
		{
			get
			{
				return this._parryHurtDeRatio;
			}
			set
			{
				this._parryHurtDeRatio = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "fighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
