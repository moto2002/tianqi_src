using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(183), ForSend(183), ProtoContract(Name = "RedPacketNty")]
	[Serializable]
	public class RedPacketNty : IExtensible
	{
		public static readonly short OP = 183;

		private RedPacketInfos _redPackets;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "redPackets", DataFormat = DataFormat.Default), DefaultValue(null)]
		public RedPacketInfos redPackets
		{
			get
			{
				return this._redPackets;
			}
			set
			{
				this._redPackets = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
