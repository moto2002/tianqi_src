using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhuChengPeiZhi")]
	[Serializable]
	public class ZhuChengPeiZhi : IExtensible
	{
		private int _scene;

		private int _name;

		private int _mapType;

		private int _openTaskId;

		private readonly List<int> _mainSenceBornPoint = new List<int>();

		private int _mainSceneBornArea;

		private int _visualField;

		private readonly List<int> _transferPoint = new List<int>();

		private readonly List<int> _transferBornPoint = new List<int>();

		private int _mainSenceIcon;

		private readonly List<int> _mainSenceIconPoint = new List<int>();

		private string _miniMap = string.Empty;

		private readonly List<float> _anchorPoint = new List<float>();

		private int _title;

		private int _listDisplay;

		private int _teleport;

		private int _interface;

		private int _sort;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "scene", DataFormat = DataFormat.TwosComplement)]
		public int scene
		{
			get
			{
				return this._scene;
			}
			set
			{
				this._scene = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "mapType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mapType
		{
			get
			{
				return this._mapType;
			}
			set
			{
				this._mapType = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "openTaskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openTaskId
		{
			get
			{
				return this._openTaskId;
			}
			set
			{
				this._openTaskId = value;
			}
		}

		[ProtoMember(6, Name = "mainSenceBornPoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> mainSenceBornPoint
		{
			get
			{
				return this._mainSenceBornPoint;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "mainSceneBornArea", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mainSceneBornArea
		{
			get
			{
				return this._mainSceneBornArea;
			}
			set
			{
				this._mainSceneBornArea = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "visualField", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int visualField
		{
			get
			{
				return this._visualField;
			}
			set
			{
				this._visualField = value;
			}
		}

		[ProtoMember(9, Name = "transferPoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> transferPoint
		{
			get
			{
				return this._transferPoint;
			}
		}

		[ProtoMember(10, Name = "transferBornPoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> transferBornPoint
		{
			get
			{
				return this._transferBornPoint;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "mainSenceIcon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mainSenceIcon
		{
			get
			{
				return this._mainSenceIcon;
			}
			set
			{
				this._mainSenceIcon = value;
			}
		}

		[ProtoMember(12, Name = "mainSenceIconPoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> mainSenceIconPoint
		{
			get
			{
				return this._mainSenceIconPoint;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "miniMap", DataFormat = DataFormat.Default), DefaultValue("")]
		public string miniMap
		{
			get
			{
				return this._miniMap;
			}
			set
			{
				this._miniMap = value;
			}
		}

		[ProtoMember(14, Name = "anchorPoint", DataFormat = DataFormat.FixedSize)]
		public List<float> anchorPoint
		{
			get
			{
				return this._anchorPoint;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "title", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int title
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

		[ProtoMember(16, IsRequired = false, Name = "listDisplay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int listDisplay
		{
			get
			{
				return this._listDisplay;
			}
			set
			{
				this._listDisplay = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "teleport", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int teleport
		{
			get
			{
				return this._teleport;
			}
			set
			{
				this._teleport = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "interface", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int @interface
		{
			get
			{
				return this._interface;
			}
			set
			{
				this._interface = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "sort", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sort
		{
			get
			{
				return this._sort;
			}
			set
			{
				this._sort = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
