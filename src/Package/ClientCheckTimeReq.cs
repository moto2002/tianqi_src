using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(100), ForSend(100), ProtoContract(Name = "ClientCheckTimeReq")]
	[Serializable]
	public class ClientCheckTimeReq : IExtensible
	{
		public static readonly short OP = 100;

		private int _clientTime;

		private string _str = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "clientTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int clientTime
		{
			get
			{
				return this._clientTime;
			}
			set
			{
				this._clientTime = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "str", DataFormat = DataFormat.Default), DefaultValue("")]
		public string str
		{
			get
			{
				return this._str;
			}
			set
			{
				this._str = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
