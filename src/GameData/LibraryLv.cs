using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "LibraryLv")]
	[Serializable]
	public class LibraryLv : IExtensible
	{
		[ProtoContract(Name = "SlotopenPair")]
		[Serializable]
		public class SlotopenPair : IExtensible
		{
			private int _key;

			private string _value = string.Empty;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
			public string value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _id;

		private int _libraryID;

		private int _libraryLv;

		private int _count;

		private readonly List<LibraryLv.SlotopenPair> _slotOpen = new List<LibraryLv.SlotopenPair>();

		private int _nextSlot;

		private int _chineseData;

		private int _icon;

		private int _effect;

		private int _attributeTemplateID;

		private int _peopleAbility;

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

		[ProtoMember(2, IsRequired = true, Name = "libraryID", DataFormat = DataFormat.TwosComplement)]
		public int libraryID
		{
			get
			{
				return this._libraryID;
			}
			set
			{
				this._libraryID = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "libraryLv", DataFormat = DataFormat.TwosComplement)]
		public int libraryLv
		{
			get
			{
				return this._libraryLv;
			}
			set
			{
				this._libraryLv = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "count", DataFormat = DataFormat.TwosComplement)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(5, Name = "slotOpen", DataFormat = DataFormat.Default)]
		public List<LibraryLv.SlotopenPair> slotOpen
		{
			get
			{
				return this._slotOpen;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "nextSlot", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nextSlot
		{
			get
			{
				return this._nextSlot;
			}
			set
			{
				this._nextSlot = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "chineseData", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chineseData
		{
			get
			{
				return this._chineseData;
			}
			set
			{
				this._chineseData = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "effect", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "attributeTemplateID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "peopleAbility", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
