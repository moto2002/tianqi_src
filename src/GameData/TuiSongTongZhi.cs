using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TuiSongTongZhi")]
	[Serializable]
	public class TuiSongTongZhi : IExtensible
	{
		private int _id;

		private int _sysId;

		private int _activityid;

		private int _early;

		private int _open;

		private int _serverPush;

		private int _type;

		private int _detail;

		private string _tab = string.Empty;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "sysId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sysId
		{
			get
			{
				return this._sysId;
			}
			set
			{
				this._sysId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "activityid", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activityid
		{
			get
			{
				return this._activityid;
			}
			set
			{
				this._activityid = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "early", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int early
		{
			get
			{
				return this._early;
			}
			set
			{
				this._early = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "open", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int open
		{
			get
			{
				return this._open;
			}
			set
			{
				this._open = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "serverPush", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int serverPush
		{
			get
			{
				return this._serverPush;
			}
			set
			{
				this._serverPush = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
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

		[ProtoMember(9, IsRequired = false, Name = "detail", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int detail
		{
			get
			{
				return this._detail;
			}
			set
			{
				this._detail = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "tab", DataFormat = DataFormat.Default), DefaultValue("")]
		public string tab
		{
			get
			{
				return this._tab;
			}
			set
			{
				this._tab = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
