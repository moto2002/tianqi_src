using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "ArenaMirrorRole")]
	[Serializable]
	public class ArenaMirrorRole : IExtensible
	{
		private long _roleId;

		private int _lv;

		private int _career;

		private int _titleId;

		private int _petYoke;

		private string _name;

		private readonly List<ArenaMirrorPet> _arenaMirrorPets = new List<ArenaMirrorPet>();

		private readonly List<MirrorEquip> _mirrorEquips = new List<MirrorEquip>();

		private readonly List<MirrorGem> _mirrorGems = new List<MirrorGem>();

		private readonly List<KeyAndValue> _skills = new List<KeyAndValue>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

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

		[ProtoMember(3, IsRequired = true, Name = "career", DataFormat = DataFormat.TwosComplement)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "titleId", DataFormat = DataFormat.TwosComplement)]
		public int titleId
		{
			get
			{
				return this._titleId;
			}
			set
			{
				this._titleId = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "petYoke", DataFormat = DataFormat.TwosComplement)]
		public int petYoke
		{
			get
			{
				return this._petYoke;
			}
			set
			{
				this._petYoke = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(7, Name = "arenaMirrorPets", DataFormat = DataFormat.Default)]
		public List<ArenaMirrorPet> arenaMirrorPets
		{
			get
			{
				return this._arenaMirrorPets;
			}
		}

		[ProtoMember(8, Name = "mirrorEquips", DataFormat = DataFormat.Default)]
		public List<MirrorEquip> mirrorEquips
		{
			get
			{
				return this._mirrorEquips;
			}
		}

		[ProtoMember(9, Name = "mirrorGems", DataFormat = DataFormat.Default)]
		public List<MirrorGem> mirrorGems
		{
			get
			{
				return this._mirrorGems;
			}
		}

		[ProtoMember(10, Name = "skills", DataFormat = DataFormat.Default)]
		public List<KeyAndValue> skills
		{
			get
			{
				return this._skills;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
