using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(147), ForSend(147), ProtoContract(Name = "SendGivingFashion")]
	[Serializable]
	public class SendGivingFashion : IExtensible
	{
		public static readonly short OP = 147;

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
