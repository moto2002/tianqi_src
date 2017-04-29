using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "RoleInfo")]
	[Serializable]
	public class RoleInfo : IExtensible
	{
		private long _roleId;

		private string _roleName;

		private int _lv;

		private long _exp;

		private long _expLmt;

		private int _typeId;

		private int _modelId;

		private CityBaseInfo _cityInfo;

		private RoleGenderType.GENDER_TYPE _gender;

		private int _rankValue;

		private readonly List<BattleSkillInfo> _skills = new List<BattleSkillInfo>();

		private int _loginTimes;

		private int _loginTime;

		private int _lastLoginTime;

		private int _lastLogoutTime;

		private int _mapId;

		private Pos _pos;

		private Vector2 _vector;

		private bool _isFirstLogin;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "roleName", DataFormat = DataFormat.Default)]
		public string roleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				this._roleName = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "exp", DataFormat = DataFormat.TwosComplement)]
		public long exp
		{
			get
			{
				return this._exp;
			}
			set
			{
				this._exp = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "expLmt", DataFormat = DataFormat.TwosComplement)]
		public long expLmt
		{
			get
			{
				return this._expLmt;
			}
			set
			{
				this._expLmt = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "modelId", DataFormat = DataFormat.TwosComplement)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "cityInfo", DataFormat = DataFormat.Default)]
		public CityBaseInfo cityInfo
		{
			get
			{
				return this._cityInfo;
			}
			set
			{
				this._cityInfo = value;
			}
		}

		[ProtoMember(12, IsRequired = true, Name = "gender", DataFormat = DataFormat.TwosComplement)]
		public RoleGenderType.GENDER_TYPE gender
		{
			get
			{
				return this._gender;
			}
			set
			{
				this._gender = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "rankValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rankValue
		{
			get
			{
				return this._rankValue;
			}
			set
			{
				this._rankValue = value;
			}
		}

		[ProtoMember(66, Name = "skills", DataFormat = DataFormat.Default)]
		public List<BattleSkillInfo> skills
		{
			get
			{
				return this._skills;
			}
		}

		[ProtoMember(100, IsRequired = false, Name = "loginTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int loginTimes
		{
			get
			{
				return this._loginTimes;
			}
			set
			{
				this._loginTimes = value;
			}
		}

		[ProtoMember(101, IsRequired = false, Name = "loginTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int loginTime
		{
			get
			{
				return this._loginTime;
			}
			set
			{
				this._loginTime = value;
			}
		}

		[ProtoMember(102, IsRequired = false, Name = "lastLoginTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastLoginTime
		{
			get
			{
				return this._lastLoginTime;
			}
			set
			{
				this._lastLoginTime = value;
			}
		}

		[ProtoMember(103, IsRequired = false, Name = "lastLogoutTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastLogoutTime
		{
			get
			{
				return this._lastLogoutTime;
			}
			set
			{
				this._lastLogoutTime = value;
			}
		}

		[ProtoMember(120, IsRequired = false, Name = "mapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mapId
		{
			get
			{
				return this._mapId;
			}
			set
			{
				this._mapId = value;
			}
		}

		[ProtoMember(121, IsRequired = false, Name = "pos", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Pos pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		[ProtoMember(122, IsRequired = false, Name = "vector", DataFormat = DataFormat.Default), DefaultValue(null)]
		public Vector2 vector
		{
			get
			{
				return this._vector;
			}
			set
			{
				this._vector = value;
			}
		}

		[ProtoMember(123, IsRequired = false, Name = "isFirstLogin", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isFirstLogin
		{
			get
			{
				return this._isFirstLogin;
			}
			set
			{
				this._isFirstLogin = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
