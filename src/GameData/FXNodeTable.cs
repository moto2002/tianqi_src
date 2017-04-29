using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "FXNodeTable")]
	[Serializable]
	public class FXNodeTable : IExtensible
	{
		private int _id;

		private int _uiId;

		private string _nodeName = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "uiId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int uiId
		{
			get
			{
				return this._uiId;
			}
			set
			{
				this._uiId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "nodeName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string nodeName
		{
			get
			{
				return this._nodeName;
			}
			set
			{
				this._nodeName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
