using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6500), ForSend(6500), ProtoContract(Name = "UpFashionRes")]
	[Serializable]
	public class UpFashionRes : IExtensible
	{
		public static readonly short OP = 6500;

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
