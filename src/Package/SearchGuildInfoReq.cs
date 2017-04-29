using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(749), ForSend(749), ProtoContract(Name = "SearchGuildInfoReq")]
	[Serializable]
	public class SearchGuildInfoReq : IExtensible
	{
		public static readonly short OP = 749;

		private string _name;

		private int _nPage = 1;

		private bool _canJoin;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
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

		[ProtoMember(2, IsRequired = false, Name = "nPage", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int nPage
		{
			get
			{
				return this._nPage;
			}
			set
			{
				this._nPage = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "canJoin", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool canJoin
		{
			get
			{
				return this._canJoin;
			}
			set
			{
				this._canJoin = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
