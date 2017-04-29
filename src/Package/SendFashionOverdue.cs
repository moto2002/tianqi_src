using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(141), ForSend(141), ProtoContract(Name = "SendFashionOverdue")]
	[Serializable]
	public class SendFashionOverdue : IExtensible
	{
		public static readonly short OP = 141;

		private string _fashionId = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "fashionId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string fashionId
		{
			get
			{
				return this._fashionId;
			}
			set
			{
				this._fashionId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
