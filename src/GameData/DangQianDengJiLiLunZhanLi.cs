using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DangQianDengJiLiLunZhanLi")]
	[Serializable]
	public class DangQianDengJiLiLunZhanLi : IExtensible
	{
		private int _actorLv;

		private int _equip;

		private int _equipExplain;

		private int _strengthen;

		private int _strengthenExplain;

		private int _equipStar;

		private int _equipStarExplain;

		private int _enchant;

		private int _enchantExplain;

		private int _diamond;

		private int _diamondExplain;

		private int _wing;

		private int _wingExplain;

		private int _petLv;

		private int _petLvExplain;

		private int _petStar;

		private int _petStarExplain;

		private int _shenBing;

		private int _shenBingExplain;

		private int _idealStrength;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "actorLv", DataFormat = DataFormat.TwosComplement)]
		public int actorLv
		{
			get
			{
				return this._actorLv;
			}
			set
			{
				this._actorLv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "equip", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equip
		{
			get
			{
				return this._equip;
			}
			set
			{
				this._equip = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "equipExplain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equipExplain
		{
			get
			{
				return this._equipExplain;
			}
			set
			{
				this._equipExplain = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "strengthen", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int strengthen
		{
			get
			{
				return this._strengthen;
			}
			set
			{
				this._strengthen = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "strengthenExplain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int strengthenExplain
		{
			get
			{
				return this._strengthenExplain;
			}
			set
			{
				this._strengthenExplain = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "equipStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equipStar
		{
			get
			{
				return this._equipStar;
			}
			set
			{
				this._equipStar = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "equipStarExplain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equipStarExplain
		{
			get
			{
				return this._equipStarExplain;
			}
			set
			{
				this._equipStarExplain = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "enchant", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int enchant
		{
			get
			{
				return this._enchant;
			}
			set
			{
				this._enchant = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "enchantExplain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int enchantExplain
		{
			get
			{
				return this._enchantExplain;
			}
			set
			{
				this._enchantExplain = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "diamond", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int diamond
		{
			get
			{
				return this._diamond;
			}
			set
			{
				this._diamond = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "diamondExplain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int diamondExplain
		{
			get
			{
				return this._diamondExplain;
			}
			set
			{
				this._diamondExplain = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "wing", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int wing
		{
			get
			{
				return this._wing;
			}
			set
			{
				this._wing = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "wingExplain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int wingExplain
		{
			get
			{
				return this._wingExplain;
			}
			set
			{
				this._wingExplain = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "petLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(16, IsRequired = false, Name = "petLvExplain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petLvExplain
		{
			get
			{
				return this._petLvExplain;
			}
			set
			{
				this._petLvExplain = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "petStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(18, IsRequired = false, Name = "petStarExplain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petStarExplain
		{
			get
			{
				return this._petStarExplain;
			}
			set
			{
				this._petStarExplain = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "shenBing", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int shenBing
		{
			get
			{
				return this._shenBing;
			}
			set
			{
				this._shenBing = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "shenBingExplain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int shenBingExplain
		{
			get
			{
				return this._shenBingExplain;
			}
			set
			{
				this._shenBingExplain = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "idealStrength", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int idealStrength
		{
			get
			{
				return this._idealStrength;
			}
			set
			{
				this._idealStrength = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
