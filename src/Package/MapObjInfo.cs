using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MapObjInfo")]
	[Serializable]
	public class MapObjInfo : IExtensible
	{
		private GameObjectType.ENUM _objType;

		private long _id;

		private long _ownerId;

		private int _typeId;

		private int _modelId;

		private string _name;

		private int _rankValue;

		private int _titleId;

		private GuildInfo _guildInfo;

		private int _mapLayer;

		private Pos _pos;

		private Vector2 _vector;

		private MapObjDecorations _decorations;

		private CityBaseInfo _cityInfo;

		private SimpleBaseInfo _otherInfo;

		private BattleBaseInfo _battleInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "objType", DataFormat = DataFormat.TwosComplement)]
		public GameObjectType.ENUM objType
		{
			get
			{
				return this._objType;
			}
			set
			{
				this._objType = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public long id
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

		[ProtoMember(3, IsRequired = false, Name = "ownerId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long ownerId
		{
			get
			{
				return this._ownerId;
			}
			set
			{
				this._ownerId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "modelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
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

		[ProtoMember(7, IsRequired = false, Name = "rankValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rankValue
		{
			get
			{
				return this._rankValue;
			}
			set
			{
				this._rankValue = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "titleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int titleId
		{
			get
			{
				return this._titleId;
			}
			set
			{
				this._titleId = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "guildInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public GuildInfo guildInfo
		{
			get
			{
				return this._guildInfo;
			}
			set
			{
				this._guildInfo = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "mapLayer", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mapLayer
		{
			get
			{
				return this._mapLayer;
			}
			set
			{
				this._mapLayer = value;
			}
		}

		[ProtoMember(21, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
		public Pos pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		[ProtoMember(22, IsRequired = true, Name = "vector", DataFormat = DataFormat.Default)]
		public Vector2 vector
		{
			get
			{
				return this._vector;
			}
			set
			{
				this._vector = value;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "decorations", DataFormat = DataFormat.Default), DefaultValue(null)]
		public MapObjDecorations decorations
		{
			get
			{
				return this._decorations;
			}
			set
			{
				this._decorations = value;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "cityInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public CityBaseInfo cityInfo
		{
			get
			{
				return this._cityInfo;
			}
			set
			{
				this._cityInfo = value;
			}
		}

		[ProtoMember(32, IsRequired = false, Name = "otherInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public SimpleBaseInfo otherInfo
		{
			get
			{
				return this._otherInfo;
			}
			set
			{
				this._otherInfo = value;
			}
		}

		[ProtoMember(33, IsRequired = false, Name = "battleInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public BattleBaseInfo battleInfo
		{
			get
			{
				return this._battleInfo;
			}
			set
			{
				this._battleInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
