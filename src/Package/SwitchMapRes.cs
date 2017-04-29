using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(517), ForSend(517), ProtoContract(Name = "SwitchMapRes")]
	[Serializable]
	public class SwitchMapRes : IExtensible
	{
		public static readonly short OP = 517;

		private int _oldMapId;

		private int _newMapId;

		private Pos _pos;

		private int _mapLayer;

		private Vector2 _vector;

		private MapObjInfo _selfObj;

		private readonly List<MapObjInfo> _otherObjs = new List<MapObjInfo>();

		private int _transformId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "oldMapId", DataFormat = DataFormat.TwosComplement)]
		public int oldMapId
		{
			get
			{
				return this._oldMapId;
			}
			set
			{
				this._oldMapId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "newMapId", DataFormat = DataFormat.TwosComplement)]
		public int newMapId
		{
			get
			{
				return this._newMapId;
			}
			set
			{
				this._newMapId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
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

		[ProtoMember(4, IsRequired = false, Name = "mapLayer", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "vector", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(6, IsRequired = false, Name = "selfObj", DataFormat = DataFormat.Default), DefaultValue(null)]
		public MapObjInfo selfObj
		{
			get
			{
				return this._selfObj;
			}
			set
			{
				this._selfObj = value;
			}
		}

		[ProtoMember(7, Name = "otherObjs", DataFormat = DataFormat.Default)]
		public List<MapObjInfo> otherObjs
		{
			get
			{
				return this._otherObjs;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "transformId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int transformId
		{
			get
			{
				return this._transformId;
			}
			set
			{
				this._transformId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
