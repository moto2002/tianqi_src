using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Scene")]
	[Serializable]
	public class Scene : IExtensible
	{
		private int _id;

		private int _sceneType;

		private string _LockLookPoint;

		private int _CameraWideAngle;

		private float _CameraDistance;

		private int _cameraDefaultAngle;

		private float _cameraAngle;

		private float _CameraHeight;

		private readonly List<float> _CameraPreset = new List<float>();

		private string _clip = string.Empty;

		private int _visualField;

		private string _path = string.Empty;

		private string _navPath = string.Empty;

		private string _CameraPath = string.Empty;

		private string _lightmap = string.Empty;

		private int _sceneFX;

		private int _music;

		private int _pointId;

		private readonly List<float> _bloomParams = new List<float>();

		private int _listlv;

		private int _name;

		private string _miniMap = string.Empty;

		private readonly List<float> _anchorPoint = new List<float>();

		private int _title;

		private int _interface;

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

		[ProtoMember(3, IsRequired = false, Name = "sceneType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sceneType
		{
			get
			{
				return this._sceneType;
			}
			set
			{
				this._sceneType = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "LockLookPoint", DataFormat = DataFormat.Default)]
		public string LockLookPoint
		{
			get
			{
				return this._LockLookPoint;
			}
			set
			{
				this._LockLookPoint = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "CameraWideAngle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int CameraWideAngle
		{
			get
			{
				return this._CameraWideAngle;
			}
			set
			{
				this._CameraWideAngle = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "CameraDistance", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float CameraDistance
		{
			get
			{
				return this._CameraDistance;
			}
			set
			{
				this._CameraDistance = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "cameraDefaultAngle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cameraDefaultAngle
		{
			get
			{
				return this._cameraDefaultAngle;
			}
			set
			{
				this._cameraDefaultAngle = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "cameraAngle", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float cameraAngle
		{
			get
			{
				return this._cameraAngle;
			}
			set
			{
				this._cameraAngle = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "CameraHeight", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float CameraHeight
		{
			get
			{
				return this._CameraHeight;
			}
			set
			{
				this._CameraHeight = value;
			}
		}

		[ProtoMember(10, Name = "CameraPreset", DataFormat = DataFormat.FixedSize)]
		public List<float> CameraPreset
		{
			get
			{
				return this._CameraPreset;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "clip", DataFormat = DataFormat.Default), DefaultValue("")]
		public string clip
		{
			get
			{
				return this._clip;
			}
			set
			{
				this._clip = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "visualField", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "path", DataFormat = DataFormat.Default), DefaultValue("")]
		public string path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "navPath", DataFormat = DataFormat.Default), DefaultValue("")]
		public string navPath
		{
			get
			{
				return this._navPath;
			}
			set
			{
				this._navPath = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "CameraPath", DataFormat = DataFormat.Default), DefaultValue("")]
		public string CameraPath
		{
			get
			{
				return this._CameraPath;
			}
			set
			{
				this._CameraPath = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "lightmap", DataFormat = DataFormat.Default), DefaultValue("")]
		public string lightmap
		{
			get
			{
				return this._lightmap;
			}
			set
			{
				this._lightmap = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "sceneFX", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sceneFX
		{
			get
			{
				return this._sceneFX;
			}
			set
			{
				this._sceneFX = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "music", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int music
		{
			get
			{
				return this._music;
			}
			set
			{
				this._music = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "pointId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pointId
		{
			get
			{
				return this._pointId;
			}
			set
			{
				this._pointId = value;
			}
		}

		[ProtoMember(20, Name = "bloomParams", DataFormat = DataFormat.FixedSize)]
		public List<float> bloomParams
		{
			get
			{
				return this._bloomParams;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "listlv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int listlv
		{
			get
			{
				return this._listlv;
			}
			set
			{
				this._listlv = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(23, IsRequired = false, Name = "miniMap", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(24, Name = "anchorPoint", DataFormat = DataFormat.FixedSize)]
		public List<float> anchorPoint
		{
			get
			{
				return this._anchorPoint;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "title", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(26, IsRequired = false, Name = "interface", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
