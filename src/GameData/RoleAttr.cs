using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id",
		"lv",
		"typeId"
	}), ProtoContract(Name = "RoleAttr")]
	[Serializable]
	public class RoleAttr : IExtensible
	{
		private int _id;

		private int _lv;

		private int _typeId;

		private int _atk;

		private int _defence;

		private int _hp;

		private int _hit;

		private int _dex;

		private int _crt;

		private int _penetration;

		private int _parry;

		private int _vigour;

		private int _attackSpeed;

		private int _ruleId;

		private int _PvpHurtDeRatio;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
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

		[ProtoMember(3, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "atk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "defence", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "hp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hp
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

		[ProtoMember(8, IsRequired = false, Name = "hit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "dex", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "crt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "penetration", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int penetration
		{
			get
			{
				return this._penetration;
			}
			set
			{
				this._penetration = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "parry", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "vigour", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(14, IsRequired = false, Name = "attackSpeed", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attackSpeed
		{
			get
			{
				return this._attackSpeed;
			}
			set
			{
				this._attackSpeed = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "ruleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ruleId
		{
			get
			{
				return this._ruleId;
			}
			set
			{
				this._ruleId = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "PvpHurtDeRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int PvpHurtDeRatio
		{
			get
			{
				return this._PvpHurtDeRatio;
			}
			set
			{
				this._PvpHurtDeRatio = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
