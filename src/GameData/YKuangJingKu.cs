using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YKuangJingKu")]
	[Serializable]
	public class YKuangJingKu : IExtensible
	{
		private int _id;

		private string _holdName = string.Empty;

		private string _Model = string.Empty;

		private readonly List<int> _item = new List<int>();

		private readonly List<int> _itemAddTime = new List<int>();

		private int _chapterId;

		private int _probability;

		private readonly List<int> _petStar = new List<int>();

		private int _depictId;

		private int _icon;

		private int _petType;

		private int _monsterLevel;

		private int _lessLevel;

		private int _maxLevel;

		private int _condition;

		private readonly List<int> _conditionValue = new List<int>();

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

		[ProtoMember(3, IsRequired = false, Name = "holdName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string holdName
		{
			get
			{
				return this._holdName;
			}
			set
			{
				this._holdName = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Model", DataFormat = DataFormat.Default), DefaultValue("")]
		public string Model
		{
			get
			{
				return this._Model;
			}
			set
			{
				this._Model = value;
			}
		}

		[ProtoMember(5, Name = "item", DataFormat = DataFormat.TwosComplement)]
		public List<int> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(6, Name = "itemAddTime", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemAddTime
		{
			get
			{
				return this._itemAddTime;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "chapterId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapterId
		{
			get
			{
				return this._chapterId;
			}
			set
			{
				this._chapterId = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		[ProtoMember(9, Name = "petStar", DataFormat = DataFormat.TwosComplement)]
		public List<int> petStar
		{
			get
			{
				return this._petStar;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "depictId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int depictId
		{
			get
			{
				return this._depictId;
			}
			set
			{
				this._depictId = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "petType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petType
		{
			get
			{
				return this._petType;
			}
			set
			{
				this._petType = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "monsterLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterLevel
		{
			get
			{
				return this._monsterLevel;
			}
			set
			{
				this._monsterLevel = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "lessLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lessLevel
		{
			get
			{
				return this._lessLevel;
			}
			set
			{
				this._lessLevel = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "maxLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxLevel
		{
			get
			{
				return this._maxLevel;
			}
			set
			{
				this._maxLevel = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "condition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int condition
		{
			get
			{
				return this._condition;
			}
			set
			{
				this._condition = value;
			}
		}

		[ProtoMember(17, Name = "conditionValue", DataFormat = DataFormat.TwosComplement)]
		public List<int> conditionValue
		{
			get
			{
				return this._conditionValue;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
