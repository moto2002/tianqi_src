using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BuddyInfoExt")]
	[Serializable]
	public class BuddyInfoExt : IExtensible
	{
		private int _hp;

		private int _atk;

		private int _defence;

		private int _pveAtk;

		private int _pvpAtk;

		private int _hitRatio;

		private int _dodgeRatio;

		private int _critRatio;

		private int _decritRatio;

		private int _critHurtAddRatio;

		private int _parryRatio;

		private int _deparryRatio;

		private int _parryHurtDeRatio;

		private int _suckBloodScale;

		private int _hurtAddRatio;

		private int _hurtDeRatio;

		private int _pveHurtAddRatio;

		private int _pveHurtDeRatio;

		private int _pvpHurtAddRatio;

		private int _pvpHurtDeRatio;

		private long _expLmt;

		private long _exp;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "hp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "atk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "defence", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "pveAtk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pveAtk
		{
			get
			{
				return this._pveAtk;
			}
			set
			{
				this._pveAtk = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "pvpAtk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pvpAtk
		{
			get
			{
				return this._pvpAtk;
			}
			set
			{
				this._pvpAtk = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "hitRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitRatio
		{
			get
			{
				return this._hitRatio;
			}
			set
			{
				this._hitRatio = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "dodgeRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dodgeRatio
		{
			get
			{
				return this._dodgeRatio;
			}
			set
			{
				this._dodgeRatio = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "critRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int critRatio
		{
			get
			{
				return this._critRatio;
			}
			set
			{
				this._critRatio = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "decritRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int decritRatio
		{
			get
			{
				return this._decritRatio;
			}
			set
			{
				this._decritRatio = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "critHurtAddRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "parryRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int parryRatio
		{
			get
			{
				return this._parryRatio;
			}
			set
			{
				this._parryRatio = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "deparryRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deparryRatio
		{
			get
			{
				return this._deparryRatio;
			}
			set
			{
				this._deparryRatio = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "parryHurtDeRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(14, IsRequired = false, Name = "suckBloodScale", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suckBloodScale
		{
			get
			{
				return this._suckBloodScale;
			}
			set
			{
				this._suckBloodScale = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "hurtAddRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hurtAddRatio
		{
			get
			{
				return this._hurtAddRatio;
			}
			set
			{
				this._hurtAddRatio = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "hurtDeRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hurtDeRatio
		{
			get
			{
				return this._hurtDeRatio;
			}
			set
			{
				this._hurtDeRatio = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "pveHurtAddRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pveHurtAddRatio
		{
			get
			{
				return this._pveHurtAddRatio;
			}
			set
			{
				this._pveHurtAddRatio = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "pveHurtDeRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pveHurtDeRatio
		{
			get
			{
				return this._pveHurtDeRatio;
			}
			set
			{
				this._pveHurtDeRatio = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "pvpHurtAddRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pvpHurtAddRatio
		{
			get
			{
				return this._pvpHurtAddRatio;
			}
			set
			{
				this._pvpHurtAddRatio = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "pvpHurtDeRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pvpHurtDeRatio
		{
			get
			{
				return this._pvpHurtDeRatio;
			}
			set
			{
				this._pvpHurtDeRatio = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "expLmt", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long expLmt
		{
			get
			{
				return this._expLmt;
			}
			set
			{
				this._expLmt = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "exp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long exp
		{
			get
			{
				return this._exp;
			}
			set
			{
				this._exp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
