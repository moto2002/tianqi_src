using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "ExtraInspection")]
	[Serializable]
	public class ExtraInspection : IExtensible
	{
		private int _id;

		private int _type;

		private int _checkType;

		private readonly List<int> _buffList = new List<int>();

		private string _percentage = string.Empty;

		private readonly List<string> _actionList = new List<string>();

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

		[ProtoMember(4, IsRequired = false, Name = "checkType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int checkType
		{
			get
			{
				return this._checkType;
			}
			set
			{
				this._checkType = value;
			}
		}

		[ProtoMember(5, Name = "buffList", DataFormat = DataFormat.TwosComplement)]
		public List<int> buffList
		{
			get
			{
				return this._buffList;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "percentage", DataFormat = DataFormat.Default), DefaultValue("")]
		public string percentage
		{
			get
			{
				return this._percentage;
			}
			set
			{
				this._percentage = value;
			}
		}

		[ProtoMember(7, Name = "actionList", DataFormat = DataFormat.Default)]
		public List<string> actionList
		{
			get
			{
				return this._actionList;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
