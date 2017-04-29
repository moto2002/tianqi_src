using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "UIWidgetTable")]
	[Serializable]
	public class UIWidgetTable : IExtensible
	{
		private int _id;

		private int _uiId;

		private string _widgetName = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(4, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = false, Name = "uiId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "widgetName", DataFormat = DataFormat.Default), DefaultValue("")]
		public string widgetName
		{
			get
			{
				return this._widgetName;
			}
			set
			{
				this._widgetName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
