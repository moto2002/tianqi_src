using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MailPreset")]
	[Serializable]
	public class MailPreset : IExtensible
	{
		private string _title = string.Empty;

		private ArticleContent _content;

		private long _startTime;

		private long _endTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "title", DataFormat = DataFormat.Default), DefaultValue("")]
		public string title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "content", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ArticleContent content
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

		[ProtoMember(3, IsRequired = false, Name = "startTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long startTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				this._startTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "endTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
