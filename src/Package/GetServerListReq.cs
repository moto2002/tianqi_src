using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(703), ForSend(703), ProtoContract(Name = "GetServerListReq")]
	[Serializable]
	public class GetServerListReq : IExtensible
	{
		public static readonly short OP = 703;

		private string _localtime = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "localtime", DataFormat = DataFormat.Default), DefaultValue("")]
		public string localtime
		{
			get
			{
				return this._localtime;
			}
			set
			{
				this._localtime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
