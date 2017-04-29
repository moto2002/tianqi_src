using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "EquipBody")]
	[Serializable]
	public class EquipBody : IExtensible
	{
		private int _id;

		private int _putOnMethod;

		private string _slot = string.Empty;

		private string _prefabPath = string.Empty;

		private string _prefabPath2 = string.Empty;

		private string _mesh = string.Empty;

		private int _listlv;

		private string _specialEfficiency = string.Empty;

		private string _specialEfficiency2 = string.Empty;

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

		[ProtoMember(7, IsRequired = false, Name = "putOnMethod", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int putOnMethod
		{
			get
			{
				return this._putOnMethod;
			}
			set
			{
				this._putOnMethod = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "slot", DataFormat = DataFormat.Default), DefaultValue("")]
		public string slot
		{
			get
			{
				return this._slot;
			}
			set
			{
				this._slot = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "prefabPath", DataFormat = DataFormat.Default), DefaultValue("")]
		public string prefabPath
		{
			get
			{
				return this._prefabPath;
			}
			set
			{
				this._prefabPath = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "prefabPath2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string prefabPath2
		{
			get
			{
				return this._prefabPath2;
			}
			set
			{
				this._prefabPath2 = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "mesh", DataFormat = DataFormat.Default), DefaultValue("")]
		public string mesh
		{
			get
			{
				return this._mesh;
			}
			set
			{
				this._mesh = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "listlv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "specialEfficiency", DataFormat = DataFormat.Default), DefaultValue("")]
		public string specialEfficiency
		{
			get
			{
				return this._specialEfficiency;
			}
			set
			{
				this._specialEfficiency = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "specialEfficiency2", DataFormat = DataFormat.Default), DefaultValue("")]
		public string specialEfficiency2
		{
			get
			{
				return this._specialEfficiency2;
			}
			set
			{
				this._specialEfficiency2 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
