using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2772), ForSend(2772), ProtoContract(Name = "OpenRoomRes")]
	[Serializable]
	public class OpenRoomRes : IExtensible
	{
		public static readonly short OP = 2772;

		private RoomInfo _roomInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "roomInfo", DataFormat = DataFormat.Default), DefaultValue(null)]
		public RoomInfo roomInfo
		{
			get
			{
				return this._roomInfo;
			}
			set
			{
				this._roomInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
