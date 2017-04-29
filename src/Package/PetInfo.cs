using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "PetInfo")]
	[Serializable]
	public class PetInfo : IExtensible
	{
		private long _id;

		private int _petId;

		private int _modelId;

		private string _name;

		private int _lv;

		private long _exp;

		private int _star;

		private int _quality;

		private readonly List<PetTalent> _petTalents = new List<PetTalent>();

		private readonly List<int> _fuseSkillIds = new List<int>();

		private readonly List<int> _talentIds = new List<int>();

		private PublicBaseInfo _publicBaseInfo;

		private int _actPointLmt;

		private int _affinity;

		private int _actPoint;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public long id
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

		[ProtoMember(2, IsRequired = true, Name = "petId", DataFormat = DataFormat.TwosComplement)]
		public int petId
		{
			get
			{
				return this._petId;
			}
			set
			{
				this._petId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "modelId", DataFormat = DataFormat.TwosComplement)]
		public int modelId
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

		[ProtoMember(4, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
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

		[ProtoMember(5, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(6, IsRequired = true, Name = "exp", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(7, IsRequired = true, Name = "star", DataFormat = DataFormat.TwosComplement)]
		public int star
		{
			get
			{
				return this._star;
			}
			set
			{
				this._star = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "quality", DataFormat = DataFormat.TwosComplement)]
		public int quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this._quality = value;
			}
		}

		[ProtoMember(9, Name = "petTalents", DataFormat = DataFormat.Default)]
		public List<PetTalent> petTalents
		{
			get
			{
				return this._petTalents;
			}
		}

		[ProtoMember(10, Name = "fuseSkillIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> fuseSkillIds
		{
			get
			{
				return this._fuseSkillIds;
			}
		}

		[ProtoMember(11, Name = "talentIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> talentIds
		{
			get
			{
				return this._talentIds;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "publicBaseInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public PublicBaseInfo publicBaseInfo
		{
			get
			{
				return this._publicBaseInfo;
			}
			set
			{
				this._publicBaseInfo = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "actPointLmt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int actPointLmt
		{
			get
			{
				return this._actPointLmt;
			}
			set
			{
				this._actPointLmt = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "affinity", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int affinity
		{
			get
			{
				return this._affinity;
			}
			set
			{
				this._affinity = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "actPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int actPoint
		{
			get
			{
				return this._actPoint;
			}
			set
			{
				this._actPoint = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
