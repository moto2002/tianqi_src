using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(744), ForSend(744), ProtoContract(Name = "QueryGuildInfoReq")]
	[Serializable]
	public class QueryGuildInfoReq : IExtensible
	{
		public static readonly short OP = 744;

		private int _fromIndex;

		private int _toIndex;

		private bool _canJoin;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "fromIndex", DataFormat = DataFormat.TwosComplement)]
		public int fromIndex
		{
			get
			{
				return this._fromIndex;
			}
			set
			{
				this._fromIndex = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "toIndex", DataFormat = DataFormat.TwosComplement)]
		public int toIndex
		{
			get
			{
				return this._toIndex;
			}
			set
			{
				this._toIndex = value;
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
