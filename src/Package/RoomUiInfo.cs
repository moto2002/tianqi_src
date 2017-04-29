using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "RoomUiInfo")]
	[Serializable]
	public class RoomUiInfo : IExtensible
	{
		private int _roomId;

		private int _playerNums;

		private int _teamFlag;

		private bool _revenge;

		private int _revengeTimeSec;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roomId", DataFormat = DataFormat.TwosComplement)]
		public int roomId
		{
			get
			{
				return this._roomId;
			}
			set
			{
				this._roomId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "playerNums", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int playerNums
		{
			get
			{
				return this._playerNums;
			}
			set
			{
				this._playerNums = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "teamFlag", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int teamFlag
		{
			get
			{
				return this._teamFlag;
			}
			set
			{
				this._teamFlag = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "revenge", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool revenge
		{
			get
			{
				return this._revenge;
			}
			set
			{
				this._revenge = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "revengeTimeSec", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int revengeTimeSec
		{
			get
			{
				return this._revengeTimeSec;
			}
			set
			{
				this._revengeTimeSec = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
