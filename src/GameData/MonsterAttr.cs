using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "MonsterAttr")]
	[Serializable]
	public class MonsterAttr : IExtensible
	{
		private int _lv;

		private int _id;

		private long _hp;

		private int _atk;

		private int _defence;

		private int _hit;

		private int _dex;

		private int _crt;

		private int _penetration;

		private int _critHurtAddRatio;

		private int _parry;

		private int _vigour;

		private int _parryHurtDeRatio;

		private int _attackSpeed;

		private int _Vp;

		private int _IdleVpResume;

		private int _VpResume;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "hp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(6, IsRequired = false, Name = "atk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "defence", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(12, IsRequired = false, Name = "critHurtAddRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "parry", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(14, IsRequired = false, Name = "vigour", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(15, IsRequired = false, Name = "parryHurtDeRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(16, IsRequired = false, Name = "attackSpeed", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(17, IsRequired = false, Name = "Vp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Vp
		{
			get
			{
				return this._Vp;
			}
			set
			{
				this._Vp = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "IdleVpResume", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int IdleVpResume
		{
			get
			{
				return this._IdleVpResume;
			}
			set
			{
				this._IdleVpResume = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "VpResume", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int VpResume
		{
			get
			{
				return this._VpResume;
			}
			set
			{
				this._VpResume = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
