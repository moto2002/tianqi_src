using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"targetId"
	}), ProtoContract(Name = "Target")]
	[Serializable]
	public class Target : IExtensible
	{
		private int _targetId;

		private int _type;

		private int _rank;

		private int _profession;

		private int _function;

		private int _element;

		private string _HPpercentage = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "targetId", DataFormat = DataFormat.TwosComplement)]
		public int targetId
		{
			get
			{
				return this._targetId;
			}
			set
			{
				this._targetId = value;
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

		[ProtoMember(4, IsRequired = false, Name = "rank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				this._rank = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "profession", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int profession
		{
			get
			{
				return this._profession;
			}
			set
			{
				this._profession = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "function", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int function
		{
			get
			{
				return this._function;
			}
			set
			{
				this._function = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "element", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int element
		{
			get
			{
				return this._element;
			}
			set
			{
				this._element = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "HPpercentage", DataFormat = DataFormat.Default), DefaultValue("")]
		public string HPpercentage
		{
			get
			{
				return this._HPpercentage;
			}
			set
			{
				this._HPpercentage = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
