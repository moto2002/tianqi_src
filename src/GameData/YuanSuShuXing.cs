using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YuanSuShuXing")]
	[Serializable]
	public class YuanSuShuXing : IExtensible
	{
		private int _element;

		private int _type;

		private int _subType;

		private readonly List<int> _target = new List<int>();

		private int _lv;

		private int _lvLmt;

		private readonly List<int> _preElementLmt = new List<int>();

		private readonly List<int> _itemId = new List<int>();

		private readonly List<int> _itemNum = new List<int>();

		private int _desc;

		private int _desc2;

		private int _desc3;

		private int _attributeTemplateID;

		private int _peopleAbility;

		private int _petAbility;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "element", DataFormat = DataFormat.TwosComplement)]
		public int element
		{
			get
			{
				return this._element;
			}
			set
			{
				this._element = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "subType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int subType
		{
			get
			{
				return this._subType;
			}
			set
			{
				this._subType = value;
			}
		}

		[ProtoMember(5, Name = "target", DataFormat = DataFormat.TwosComplement)]
		public List<int> target
		{
			get
			{
				return this._target;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "lvLmt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lvLmt
		{
			get
			{
				return this._lvLmt;
			}
			set
			{
				this._lvLmt = value;
			}
		}

		[ProtoMember(8, Name = "preElementLmt", DataFormat = DataFormat.TwosComplement)]
		public List<int> preElementLmt
		{
			get
			{
				return this._preElementLmt;
			}
		}

		[ProtoMember(9, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemId
		{
			get
			{
				return this._itemId;
			}
		}

		[ProtoMember(10, Name = "itemNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemNum
		{
			get
			{
				return this._itemNum;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "desc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "desc2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc2
		{
			get
			{
				return this._desc2;
			}
			set
			{
				this._desc2 = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "desc3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc3
		{
			get
			{
				return this._desc3;
			}
			set
			{
				this._desc3 = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "attributeTemplateID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(15, IsRequired = false, Name = "peopleAbility", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(16, IsRequired = false, Name = "petAbility", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
