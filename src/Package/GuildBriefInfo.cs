using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GuildBriefInfo")]
	[Serializable]
	public class GuildBriefInfo : IExtensible
	{
		[ProtoContract(Name = "ApplyStatus")]
		public enum ApplyStatus
		{
			[ProtoEnum(Name = "Available", Value = 1)]
			Available = 1,
			[ProtoEnum(Name = "Applying", Value = 2)]
			Applying,
			[ProtoEnum(Name = "Unavailable", Value = 3)]
			Unavailable,
			[ProtoEnum(Name = "Invited", Value = 4)]
			Invited
		}

		private long _guildId;

		private string _name;

		private int _lv;

		private MemberInfo _chairman;

		private long _fighting;

		private int _avatar;

		private GuildBriefInfo.ApplyStatus _status;

		private int _size;

		private int _rank;

		private int _roleMinLv;

		private string _notice = string.Empty;

		private int _applicantCd;

		private int _lastWarRank;

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

		[ProtoMember(4, IsRequired = true, Name = "chairman", DataFormat = DataFormat.Default)]
		public MemberInfo chairman
		{
			get
			{
				return this._chairman;
			}
			set
			{
				this._chairman = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "fighting", DataFormat = DataFormat.TwosComplement)]
		public long fighting
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

		[ProtoMember(6, IsRequired = true, Name = "avatar", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(7, IsRequired = true, Name = "status", DataFormat = DataFormat.TwosComplement)]
		public GuildBriefInfo.ApplyStatus status
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

		[ProtoMember(8, IsRequired = true, Name = "size", DataFormat = DataFormat.TwosComplement)]
		public int size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		[ProtoMember(10, IsRequired = true, Name = "rank", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(11, IsRequired = true, Name = "roleMinLv", DataFormat = DataFormat.TwosComplement)]
		public int roleMinLv
		{
			get
			{
				return this._roleMinLv;
			}
			set
			{
				this._roleMinLv = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "notice", DataFormat = DataFormat.Default), DefaultValue("")]
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

		[ProtoMember(13, IsRequired = false, Name = "applicantCd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int applicantCd
		{
			get
			{
				return this._applicantCd;
			}
			set
			{
				this._applicantCd = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "lastWarRank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastWarRank
		{
			get
			{
				return this._lastWarRank;
			}
			set
			{
				this._lastWarRank = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
