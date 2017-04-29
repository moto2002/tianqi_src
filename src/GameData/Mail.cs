using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Mail")]
	[Serializable]
	public class Mail : IExtensible
	{
		private uint _id;

		private int _type;

		private string _src = string.Empty;

		private int _to;

		private string _title = string.Empty;

		private string _text = string.Empty;

		private readonly List<int> _itemId = new List<int>();

		private readonly List<int> _itemNum = new List<int>();

		private string _startTime = string.Empty;

		private string _endTime = string.Empty;

		private int _deleteTime;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public uint id
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

		[ProtoMember(4, IsRequired = false, Name = "src", DataFormat = DataFormat.Default), DefaultValue("")]
		public string src
		{
			get
			{
				return this._src;
			}
			set
			{
				this._src = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "to", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int to
		{
			get
			{
				return this._to;
			}
			set
			{
				this._to = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "title", DataFormat = DataFormat.Default), DefaultValue("")]
		public string title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "text", DataFormat = DataFormat.Default), DefaultValue("")]
		public string text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		[ProtoMember(8, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemId
		{
			get
			{
				return this._itemId;
			}
		}

		[ProtoMember(9, Name = "itemNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemNum
		{
			get
			{
				return this._itemNum;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "startTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string startTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				this._startTime = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "endTime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "deleteTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deleteTime
		{
			get
			{
				return this._deleteTime;
			}
			set
			{
				this._deleteTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
