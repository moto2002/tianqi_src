using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "WenZiRGB")]
	[Serializable]
	public class WenZiRGB : IExtensible
	{
		private int _id;

		private string _rgb = string.Empty;

		private int _colorWordId;

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "rgb", DataFormat = DataFormat.Default), DefaultValue("")]
		public string rgb
		{
			get
			{
				return this._rgb;
			}
			set
			{
				this._rgb = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "colorWordId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int colorWordId
		{
			get
			{
				return this._colorWordId;
			}
			set
			{
				this._colorWordId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
