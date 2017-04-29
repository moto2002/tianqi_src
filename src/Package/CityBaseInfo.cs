using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "CityBaseInfo")]
	[Serializable]
	public class CityBaseInfo : IExtensible
	{
		private PublicBaseInfo _baseInfo;

		private int _Energy;

		private int _EnergyLmt;

		private long _Exp;

		private long _ExpLmt;

		private int _Diamond;

		private long _Gold;

		private int _RechargeDiamond;

		private int _Honor;

		private int _CompetitiveCurrency;

		private int _SkillPoint;

		private int _Reputation;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "baseInfo", DataFormat = DataFormat.Default)]
		public PublicBaseInfo baseInfo
		{
			get
			{
				return this._baseInfo;
			}
			set
			{
				this._baseInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "Energy", DataFormat = DataFormat.TwosComplement)]
		public int Energy
		{
			get
			{
				return this._Energy;
			}
			set
			{
				this._Energy = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "EnergyLmt", DataFormat = DataFormat.TwosComplement)]
		public int EnergyLmt
		{
			get
			{
				return this._EnergyLmt;
			}
			set
			{
				this._EnergyLmt = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "Exp", DataFormat = DataFormat.TwosComplement)]
		public long Exp
		{
			get
			{
				return this._Exp;
			}
			set
			{
				this._Exp = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "ExpLmt", DataFormat = DataFormat.TwosComplement)]
		public long ExpLmt
		{
			get
			{
				return this._ExpLmt;
			}
			set
			{
				this._ExpLmt = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "Diamond", DataFormat = DataFormat.TwosComplement)]
		public int Diamond
		{
			get
			{
				return this._Diamond;
			}
			set
			{
				this._Diamond = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "Gold", DataFormat = DataFormat.TwosComplement)]
		public long Gold
		{
			get
			{
				return this._Gold;
			}
			set
			{
				this._Gold = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "RechargeDiamond", DataFormat = DataFormat.TwosComplement)]
		public int RechargeDiamond
		{
			get
			{
				return this._RechargeDiamond;
			}
			set
			{
				this._RechargeDiamond = value;
			}
		}

		[ProtoMember(9, IsRequired = true, Name = "Honor", DataFormat = DataFormat.TwosComplement)]
		public int Honor
		{
			get
			{
				return this._Honor;
			}
			set
			{
				this._Honor = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "CompetitiveCurrency", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int CompetitiveCurrency
		{
			get
			{
				return this._CompetitiveCurrency;
			}
			set
			{
				this._CompetitiveCurrency = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "SkillPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int SkillPoint
		{
			get
			{
				return this._SkillPoint;
			}
			set
			{
				this._SkillPoint = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "Reputation", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Reputation
		{
			get
			{
				return this._Reputation;
			}
			set
			{
				this._Reputation = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
