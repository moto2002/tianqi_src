using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DBChapterAwardInfo")]
	[Serializable]
	public class DBChapterAwardInfo : IExtensible
	{
		private int _chapterAwardId;

		private bool _canReceive;

		private bool _isReceived;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "chapterAwardId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapterAwardId
		{
			get
			{
				return this._chapterAwardId;
			}
			set
			{
				this._chapterAwardId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "canReceive", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool canReceive
		{
			get
			{
				return this._canReceive;
			}
			set
			{
				this._canReceive = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "isReceived", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isReceived
		{
			get
			{
				return this._isReceived;
			}
			set
			{
				this._isReceived = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
