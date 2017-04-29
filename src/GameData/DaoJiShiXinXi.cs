using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DaoJiShiXinXi")]
	[Serializable]
	public class DaoJiShiXinXi : IExtensible
	{
		private int _messageId;

		private string _info = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "messageId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int messageId
		{
			get
			{
				return this._messageId;
			}
			set
			{
				this._messageId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "info", DataFormat = DataFormat.Default), DefaultValue("")]
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
