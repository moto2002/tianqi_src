using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "PartMonster")]
	[Serializable]
	public class PartMonster : IExtensible
	{
		private uint _id;

		private string _hangPoint = string.Empty;

		private string _suffix = string.Empty;

		private int _hangPointWay;

		private readonly List<int> _colider = new List<int>();

		private readonly List<float> _localPosition = new List<float>();

		private readonly List<float> _localRotation = new List<float>();

		private readonly List<float> _localScale = new List<float>();

		private readonly List<string> _texture = new List<string>();

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

		[ProtoMember(3, IsRequired = false, Name = "hangPoint", DataFormat = DataFormat.Default), DefaultValue("")]
		public string hangPoint
		{
			get
			{
				return this._hangPoint;
			}
			set
			{
				this._hangPoint = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "suffix", DataFormat = DataFormat.Default), DefaultValue("")]
		public string suffix
		{
			get
			{
				return this._suffix;
			}
			set
			{
				this._suffix = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "hangPointWay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hangPointWay
		{
			get
			{
				return this._hangPointWay;
			}
			set
			{
				this._hangPointWay = value;
			}
		}

		[ProtoMember(6, Name = "colider", DataFormat = DataFormat.TwosComplement)]
		public List<int> colider
		{
			get
			{
				return this._colider;
			}
		}

		[ProtoMember(7, Name = "localPosition", DataFormat = DataFormat.FixedSize)]
		public List<float> localPosition
		{
			get
			{
				return this._localPosition;
			}
		}

		[ProtoMember(8, Name = "localRotation", DataFormat = DataFormat.FixedSize)]
		public List<float> localRotation
		{
			get
			{
				return this._localRotation;
			}
		}

		[ProtoMember(9, Name = "localScale", DataFormat = DataFormat.FixedSize)]
		public List<float> localScale
		{
			get
			{
				return this._localScale;
			}
		}

		[ProtoMember(10, Name = "texture", DataFormat = DataFormat.Default)]
		public List<string> texture
		{
			get
			{
				return this._texture;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
