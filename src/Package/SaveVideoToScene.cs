using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(159), ForSend(159), ProtoContract(Name = "SaveVideoToScene")]
	[Serializable]
	public class SaveVideoToScene : IExtensible
	{
		public static readonly short OP = 159;

		private long _videoUId;

		private byte[] _videoData;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "videoUId", DataFormat = DataFormat.TwosComplement)]
		public long videoUId
		{
			get
			{
				return this._videoUId;
			}
			set
			{
				this._videoUId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "videoData", DataFormat = DataFormat.Default), DefaultValue(null)]
		public byte[] videoData
		{
			get
			{
				return this._videoData;
			}
			set
			{
				this._videoData = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
