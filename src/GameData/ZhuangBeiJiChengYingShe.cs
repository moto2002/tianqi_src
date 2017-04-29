using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhuangBeiJiChengYingShe")]
	[Serializable]
	public class ZhuangBeiJiChengYingShe : IExtensible
	{
		private int _id;

		private string _equipTransFrom = string.Empty;

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

		[ProtoMember(3, IsRequired = false, Name = "equipTransFrom", DataFormat = DataFormat.Default), DefaultValue("")]
		public string equipTransFrom
		{
			get
			{
				return this._equipTransFrom;
			}
			set
			{
				this._equipTransFrom = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
