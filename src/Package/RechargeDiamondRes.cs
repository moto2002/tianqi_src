using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1129), ForSend(1129), ProtoContract(Name = "RechargeDiamondRes")]
	[Serializable]
	public class RechargeDiamondRes : IExtensible
	{
		public static readonly short OP = 1129;

		private int _id;

		private readonly List<Item> _item = new List<Item>();

		private string _info = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, Name = "item", DataFormat = DataFormat.Default)]
		public List<Item> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "info", DataFormat = DataFormat.Default), DefaultValue("")]
		public string info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
