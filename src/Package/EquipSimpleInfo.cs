using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "EquipSimpleInfo")]
	[Serializable]
	public class EquipSimpleInfo : IExtensible
	{
		private long _equipId;

		private int _cfgId;

		private int _currentExp;

		private int _advancedExp;

		private int _position = -1;

		private readonly List<ExcellentAttr> _excellentAttrs = new List<ExcellentAttr>();

		private readonly List<int> _excellentAttrIds = new List<int>();

		private int _star;

		private readonly List<ExcellentAttr> _starTemplateIdAndNums = new List<ExcellentAttr>();

		private readonly List<ExcellentAttr> _starAttrs = new List<ExcellentAttr>();

		private readonly List<ExcellentAttr> _enchantAttrs = new List<ExcellentAttr>();

		private readonly List<int> _starToMaterial = new List<int>();

		private NoteData _refineData;

		private int _suitId = -1;

		private bool _binding;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "equipId", DataFormat = DataFormat.TwosComplement)]
		public long equipId
		{
			get
			{
				return this._equipId;
			}
			set
			{
				this._equipId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "cfgId", DataFormat = DataFormat.TwosComplement)]
		public int cfgId
		{
			get
			{
				return this._cfgId;
			}
			set
			{
				this._cfgId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "currentExp", DataFormat = DataFormat.TwosComplement)]
		public int currentExp
		{
			get
			{
				return this._currentExp;
			}
			set
			{
				this._currentExp = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "advancedExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int advancedExp
		{
			get
			{
				return this._advancedExp;
			}
			set
			{
				this._advancedExp = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "position", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
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

		[ProtoMember(6, Name = "excellentAttrs", DataFormat = DataFormat.Default)]
		public List<ExcellentAttr> excellentAttrs
		{
			get
			{
				return this._excellentAttrs;
			}
		}

		[ProtoMember(7, Name = "excellentAttrIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> excellentAttrIds
		{
			get
			{
				return this._excellentAttrIds;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "star", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, Name = "starTemplateIdAndNums", DataFormat = DataFormat.Default)]
		public List<ExcellentAttr> starTemplateIdAndNums
		{
			get
			{
				return this._starTemplateIdAndNums;
			}
		}

		[ProtoMember(10, Name = "starAttrs", DataFormat = DataFormat.Default)]
		public List<ExcellentAttr> starAttrs
		{
			get
			{
				return this._starAttrs;
			}
		}

		[ProtoMember(11, Name = "enchantAttrs", DataFormat = DataFormat.Default)]
		public List<ExcellentAttr> enchantAttrs
		{
			get
			{
				return this._enchantAttrs;
			}
		}

		[ProtoMember(12, Name = "starToMaterial", DataFormat = DataFormat.TwosComplement)]
		public List<int> starToMaterial
		{
			get
			{
				return this._starToMaterial;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "refineData", DataFormat = DataFormat.Default), DefaultValue(null)]
		public NoteData refineData
		{
			get
			{
				return this._refineData;
			}
			set
			{
				this._refineData = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "suitId", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int suitId
		{
			get
			{
				return this._suitId;
			}
			set
			{
				this._suitId = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "binding", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool binding
		{
			get
			{
				return this._binding;
			}
			set
			{
				this._binding = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
