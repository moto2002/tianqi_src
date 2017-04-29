using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "TramcarRoomInfo")]
	[Serializable]
	public class TramcarRoomInfo : IExtensible
	{
		private string _name = string.Empty;

		private int _quality;

		private int _deadlineTime;

		private long _roleId;

		private int _roleLv;

		private bool _isFriend;

		private bool _isEnemy;

		private bool _isGrab;

		private int _career;

		private int _fighting;

		private int _createTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "quality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this._quality = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "deadlineTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deadlineTime
		{
			get
			{
				return this._deadlineTime;
			}
			set
			{
				this._deadlineTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "roleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "roleLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int roleLv
		{
			get
			{
				return this._roleLv;
			}
			set
			{
				this._roleLv = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "isFriend", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isFriend
		{
			get
			{
				return this._isFriend;
			}
			set
			{
				this._isFriend = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "isEnemy", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isEnemy
		{
			get
			{
				return this._isEnemy;
			}
			set
			{
				this._isEnemy = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "isGrab", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isGrab
		{
			get
			{
				return this._isGrab;
			}
			set
			{
				this._isGrab = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "fighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fighting
		{
			get
			{
				return this._fighting;
			}
			set
			{
				this._fighting = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "createTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int createTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				this._createTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
