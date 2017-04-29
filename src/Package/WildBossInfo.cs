using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "WildBossInfo")]
	[Serializable]
	public class WildBossInfo : IExtensible
	{
		private int _idx;

		private int _bossCode;

		private Pos _pos;

		private int _status;

		private int _bossLv;

		private bool _isGroupBoss;

		private int _bossCfgId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "idx", DataFormat = DataFormat.TwosComplement)]
		public int idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "bossCode", DataFormat = DataFormat.TwosComplement)]
		public int bossCode
		{
			get
			{
				return this._bossCode;
			}
			set
			{
				this._bossCode = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
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

		[ProtoMember(5, IsRequired = true, Name = "status", DataFormat = DataFormat.TwosComplement)]
		public int status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "bossLv", DataFormat = DataFormat.TwosComplement)]
		public int bossLv
		{
			get
			{
				return this._bossLv;
			}
			set
			{
				this._bossLv = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "isGroupBoss", DataFormat = DataFormat.Default), DefaultValue(false)]
		public bool isGroupBoss
		{
			get
			{
				return this._isGroupBoss;
			}
			set
			{
				this._isGroupBoss = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "bossCfgId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossCfgId
		{
			get
			{
				return this._bossCfgId;
			}
			set
			{
				this._bossCfgId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
