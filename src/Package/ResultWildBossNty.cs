using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(611), ForSend(611), ProtoContract(Name = "ResultWildBossNty")]
	[Serializable]
	public class ResultWildBossNty : IExtensible
	{
		public static readonly short OP = 611;

		private bool _isWin;

		private readonly List<DropItem> _item = new List<DropItem>();

		private string _name = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "isWin", DataFormat = DataFormat.Default)]
		public bool isWin
		{
			get
			{
				return this._isWin;
			}
			set
			{
				this._isWin = value;
			}
		}

		[ProtoMember(2, Name = "item", DataFormat = DataFormat.Default)]
		public List<DropItem> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
