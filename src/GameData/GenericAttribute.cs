using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"lv"
	}), ProtoContract(Name = "GenericAttribute")]
	[Serializable]
	public class GenericAttribute : IExtensible
	{
		private int _lv;

		private long _personExp;

		private int _defFactor;

		private int _effectFactor;

		private int _peopleAbility;

		private int _petAbility;

		private int _petExp;

		private int _fightStandard;

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

		[ProtoMember(3, IsRequired = false, Name = "personExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long personExp
		{
			get
			{
				return this._personExp;
			}
			set
			{
				this._personExp = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "defFactor", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int defFactor
		{
			get
			{
				return this._defFactor;
			}
			set
			{
				this._defFactor = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "effectFactor", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int effectFactor
		{
			get
			{
				return this._effectFactor;
			}
			set
			{
				this._effectFactor = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "peopleAbility", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int peopleAbility
		{
			get
			{
				return this._peopleAbility;
			}
			set
			{
				this._peopleAbility = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "petAbility", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petAbility
		{
			get
			{
				return this._petAbility;
			}
			set
			{
				this._petAbility = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "petExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petExp
		{
			get
			{
				return this._petExp;
			}
			set
			{
				this._petExp = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "fightStandard", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fightStandard
		{
			get
			{
				return this._fightStandard;
			}
			set
			{
				this._fightStandard = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
