using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GuildBaseInfo")]
	[Serializable]
	public class GuildBaseInfo : IExtensible
	{
		private long _guildId;

		private string _name;

		private int _lv;

		private int _rank;

		private int _avatar;

		private string _notice = string.Empty;

		private int _memberSize;

		private long _chairmanId;

		private bool _verify;

		private bool _isMaxLv;

		private int _guildFund;

		private int _totalFighting;

		private int _builtedCount;

		private long _activity;

		private long _equipEssence;

		private int _taskedCount;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "guildId", DataFormat = DataFormat.TwosComplement)]
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this._guildId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
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

		[ProtoMember(4, IsRequired = true, Name = "rank", DataFormat = DataFormat.TwosComplement)]
		public int rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				this._rank = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "avatar", DataFormat = DataFormat.TwosComplement)]
		public int avatar
		{
			get
			{
				return this._avatar;
			}
			set
			{
				this._avatar = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "notice", DataFormat = DataFormat.Default), DefaultValue("")]
		public string notice
		{
			get
			{
				return this._notice;
			}
			set
			{
				this._notice = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "memberSize", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int memberSize
		{
			get
			{
				return this._memberSize;
			}
			set
			{
				this._memberSize = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "chairmanId", DataFormat = DataFormat.TwosComplement)]
		public long chairmanId
		{
			get
			{
				return this._chairmanId;
			}
			set
			{
				this._chairmanId = value;
			}
		}

		[ProtoMember(9, IsRequired = true, Name = "verify", DataFormat = DataFormat.Default)]
		public bool verify
		{
			get
			{
				return this._verify;
			}
			set
			{
				this._verify = value;
			}
		}

		[ProtoMember(10, IsRequired = true, Name = "isMaxLv", DataFormat = DataFormat.Default)]
		public bool isMaxLv
		{
			get
			{
				return this._isMaxLv;
			}
			set
			{
				this._isMaxLv = value;
			}
		}

		[ProtoMember(11, IsRequired = true, Name = "guildFund", DataFormat = DataFormat.TwosComplement)]
		public int guildFund
		{
			get
			{
				return this._guildFund;
			}
			set
			{
				this._guildFund = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "totalFighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int totalFighting
		{
			get
			{
				return this._totalFighting;
			}
			set
			{
				this._totalFighting = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "builtedCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int builtedCount
		{
			get
			{
				return this._builtedCount;
			}
			set
			{
				this._builtedCount = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "activity", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long activity
		{
			get
			{
				return this._activity;
			}
			set
			{
				this._activity = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "equipEssence", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long equipEssence
		{
			get
			{
				return this._equipEssence;
			}
			set
			{
				this._equipEssence = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "taskedCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskedCount
		{
			get
			{
				return this._taskedCount;
			}
			set
			{
				this._taskedCount = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
