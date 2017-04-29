using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3130), ForSend(3130), ProtoContract(Name = "RechargeSuccessNty")]
	[Serializable]
	public class RechargeSuccessNty : IExtensible
	{
		public static readonly short OP = 3130;

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
