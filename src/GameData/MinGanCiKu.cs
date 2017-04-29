using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "MinGanCiKu")]
	[Serializable]
	public class MinGanCiKu : IExtensible
	{
		private int _id;

		private string _content = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "content", DataFormat = DataFormat.Default), DefaultValue("")]
		public string content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
