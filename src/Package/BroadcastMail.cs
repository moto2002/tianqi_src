using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BroadcastMail")]
	[Serializable]
	public class BroadcastMail : IExtensible
	{
		private long _id;

		private string _title;

		private ArticleContent _content;

		private long _startTime;

		private long _endTime;

		private readonly List<long> _roleIds = new List<long>();

		private MailRecvCond _cond;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "title", DataFormat = DataFormat.Default)]
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

		[ProtoMember(4, IsRequired = false, Name = "startTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(5, IsRequired = false, Name = "endTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(6, Name = "roleIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> roleIds
		{
			get
			{
				return this._roleIds;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "cond", DataFormat = DataFormat.Default), DefaultValue(null)]
		public MailRecvCond cond
		{
			get
			{
				return this._cond;
			}
			set
			{
				this._cond = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
