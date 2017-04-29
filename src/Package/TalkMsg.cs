using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "TalkMsg")]
	[Serializable]
	public class TalkMsg : IExtensible
	{
		private ChannelType.CT _type;

		private DetailInfo _sender;

		private ArticleContent _content;

		private DetailInfo _receiver;

		private int _time;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public ChannelType.CT type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "sender", DataFormat = DataFormat.Default)]
		public DetailInfo sender
		{
			get
			{
				return this._sender;
			}
			set
			{
				this._sender = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "content", DataFormat = DataFormat.Default)]
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

		[ProtoMember(4, IsRequired = false, Name = "receiver", DataFormat = DataFormat.Default), DefaultValue(null)]
		public DetailInfo receiver
		{
			get
			{
				return this._receiver;
			}
			set
			{
				this._receiver = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
