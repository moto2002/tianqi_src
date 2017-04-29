using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "FuWen")]
	[Serializable]
	public class FuWen : IExtensible
	{
		private int _id;

		private int _attributeTemplateID;

		private readonly List<int> _syntheticMaterialID = new List<int>();

		private readonly List<int> _syntheticMaterialValue = new List<int>();

		private int _cost;

		private int _petAbility;

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

		[ProtoMember(6, IsRequired = false, Name = "attributeTemplateID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, Name = "syntheticMaterialID", DataFormat = DataFormat.TwosComplement)]
		public List<int> syntheticMaterialID
		{
			get
			{
				return this._syntheticMaterialID;
			}
		}

		[ProtoMember(8, Name = "syntheticMaterialValue", DataFormat = DataFormat.TwosComplement)]
		public List<int> syntheticMaterialValue
		{
			get
			{
				return this._syntheticMaterialValue;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "cost", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "petAbility", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
