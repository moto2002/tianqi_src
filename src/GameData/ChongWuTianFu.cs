using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChongWuTianFu")]
	[Serializable]
	public class ChongWuTianFu : IExtensible
	{
		private int _id;

		private int _name;

		private int _lvRuleId;

		private string _picture = string.Empty;

		private int _precondition1;

		private int _type;

		private int _effect;

		private readonly List<int> _parameter = new List<int>();

		private readonly List<int> _parameter2 = new List<int>();

		private int _describe;

		private float _peopleAbility;

		private float _petAbility;

		private float _allPetAbility;

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

		[ProtoMember(4, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name
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

		[ProtoMember(6, IsRequired = false, Name = "lvRuleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lvRuleId
		{
			get
			{
				return this._lvRuleId;
			}
			set
			{
				this._lvRuleId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "picture", DataFormat = DataFormat.Default), DefaultValue("")]
		public string picture
		{
			get
			{
				return this._picture;
			}
			set
			{
				this._picture = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "precondition1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int precondition1
		{
			get
			{
				return this._precondition1;
			}
			set
			{
				this._precondition1 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "effect", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int effect
		{
			get
			{
				return this._effect;
			}
			set
			{
				this._effect = value;
			}
		}

		[ProtoMember(11, Name = "parameter", DataFormat = DataFormat.TwosComplement)]
		public List<int> parameter
		{
			get
			{
				return this._parameter;
			}
		}

		[ProtoMember(12, Name = "parameter2", DataFormat = DataFormat.TwosComplement)]
		public List<int> parameter2
		{
			get
			{
				return this._parameter2;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "describe", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describe
		{
			get
			{
				return this._describe;
			}
			set
			{
				this._describe = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "peopleAbility", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float peopleAbility
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

		[ProtoMember(15, IsRequired = false, Name = "petAbility", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float petAbility
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

		[ProtoMember(16, IsRequired = false, Name = "allPetAbility", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float allPetAbility
		{
			get
			{
				return this._allPetAbility;
			}
			set
			{
				this._allPetAbility = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
