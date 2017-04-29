using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MailInfo")]
	[Serializable]
	public class MailInfo : IExtensible
	{
		private MailType.MT _type;

		private long _id;

		private int _status;

		private DetailInfo _sender;

		private readonly List<DetailInfo> _receivers = new List<DetailInfo>();

		private string _title = string.Empty;

		private ArticleContent _content;

		private long _buildDate;

		private int _drawMark;

		private int _endTime;

		private int _timeoutSec = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public MailType.MT type
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

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public long id
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

		[ProtoMember(3, IsRequired = true, Name = "status", DataFormat = DataFormat.TwosComplement)]
		public int status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "sender", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(5, Name = "receivers", DataFormat = DataFormat.Default)]
		public List<DetailInfo> receivers
		{
			get
			{
				return this._receivers;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "title", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(7, IsRequired = false, Name = "content", DataFormat = DataFormat.Default), DefaultValue(null)]
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

		[ProtoMember(8, IsRequired = false, Name = "buildDate", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long buildDate
		{
			get
			{
				return this._buildDate;
			}
			set
			{
				this._buildDate = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "drawMark", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int drawMark
		{
			get
			{
				return this._drawMark;
			}
			set
			{
				this._drawMark = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "endTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int endTime
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

		[ProtoMember(20, IsRequired = false, Name = "timeoutSec", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int timeoutSec
		{
			get
			{
				return this._timeoutSec;
			}
			set
			{
				this._timeoutSec = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
