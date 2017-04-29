using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChongWuPinZhi")]
	[Serializable]
	public class ChongWuPinZhi : IExtensible
	{
		private int _id;

		private int _color;

		private int _rune1;

		private int _rune2;

		private int _rune3;

		private int _rune4;

		private int _rune5;

		private int _rune6;

		private int _attributeTemplateID;

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

		[ProtoMember(3, IsRequired = false, Name = "color", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "rune1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rune1
		{
			get
			{
				return this._rune1;
			}
			set
			{
				this._rune1 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "rune2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rune2
		{
			get
			{
				return this._rune2;
			}
			set
			{
				this._rune2 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "rune3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rune3
		{
			get
			{
				return this._rune3;
			}
			set
			{
				this._rune3 = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "rune4", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rune4
		{
			get
			{
				return this._rune4;
			}
			set
			{
				this._rune4 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "rune5", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rune5
		{
			get
			{
				return this._rune5;
			}
			set
			{
				this._rune5 = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "rune6", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rune6
		{
			get
			{
				return this._rune6;
			}
			set
			{
				this._rune6 = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "attributeTemplateID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(12, IsRequired = false, Name = "petAbility", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
