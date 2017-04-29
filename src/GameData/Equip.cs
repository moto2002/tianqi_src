using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Equip")]
	[Serializable]
	public class Equip : IExtensible
	{
		private int _id;

		private int _peopleAbility;

		private int _position;

		private int _occupation;

		private int _model;

		private int _attributeTemplateID;

		private readonly List<int> _syntheticMaterialID = new List<int>();

		private readonly List<int> _syntheticMaterialValue = new List<int>();

		private int _cost;

		private readonly List<int> _coordinate = new List<int>();

		private int _frontID;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "peopleAbility", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "position", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "occupation", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int occupation
		{
			get
			{
				return this._occupation;
			}
			set
			{
				this._occupation = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int model
		{
			get
			{
				return this._model;
			}
			set
			{
				this._model = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "attributeTemplateID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attributeTemplateID
		{
			get
			{
				return this._attributeTemplateID;
			}
			set
			{
				this._attributeTemplateID = value;
			}
		}

		[ProtoMember(9, Name = "syntheticMaterialID", DataFormat = DataFormat.TwosComplement)]
		public List<int> syntheticMaterialID
		{
			get
			{
				return this._syntheticMaterialID;
			}
		}

		[ProtoMember(10, Name = "syntheticMaterialValue", DataFormat = DataFormat.TwosComplement)]
		public List<int> syntheticMaterialValue
		{
			get
			{
				return this._syntheticMaterialValue;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "cost", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cost
		{
			get
			{
				return this._cost;
			}
			set
			{
				this._cost = value;
			}
		}

		[ProtoMember(12, Name = "coordinate", DataFormat = DataFormat.TwosComplement)]
		public List<int> coordinate
		{
			get
			{
				return this._coordinate;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "frontID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int frontID
		{
			get
			{
				return this._frontID;
			}
			set
			{
				this._frontID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
