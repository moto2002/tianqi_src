using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MultiPvpRoleInfo")]
	[Serializable]
	public class MultiPvpRoleInfo : IExtensible
	{
		public static readonly short OP = 707;

		private long _roleId;

		private int _killCount;

		private int _deathCount;

		private int _score;

		private int _killBossCount;

		private int _maxCombo;

		private int _camp;

		private string _roleName = string.Empty;

		private int _roleLv;

		private int _career;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "roleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		[ProtoMember(2, IsRequired = false, Name = "killCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int killCount
		{
			get
			{
				return this._killCount;
			}
			set
			{
				this._killCount = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "deathCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deathCount
		{
			get
			{
				return this._deathCount;
			}
			set
			{
				this._deathCount = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "score", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int score
		{
			get
			{
				return this._score;
			}
			set
			{
				this._score = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "killBossCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int killBossCount
		{
			get
			{
				return this._killBossCount;
			}
			set
			{
				this._killBossCount = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "maxCombo", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxCombo
		{
			get
			{
				return this._maxCombo;
			}
			set
			{
				this._maxCombo = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "camp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int camp
		{
			get
			{
				return this._camp;
			}
			set
			{
				this._camp = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "roleName", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(9, IsRequired = false, Name = "roleLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
