using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MemberInfo")]
	[Serializable]
	public class MemberInfo : IExtensible
	{
		private long _roleId;

		private string _name;

		private int _lv;

		private int _modelId;

		private long _fighting;

		private readonly List<MemberTitleType.MTT> _title = new List<MemberTitleType.MTT>();

		private int _offlineSec;

		private int _lastOnlineDate;

		private int _joinGuildUtc;

		private int _appoint;

		private int _contribution;

		private int _career;

		private int _vipLv;

		private int _activity;

		private int _fund;

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

		[ProtoMember(4, IsRequired = true, Name = "modelId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(6, Name = "title", DataFormat = DataFormat.TwosComplement)]
		public List<MemberTitleType.MTT> title
		{
			get
			{
				return this._title;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "offlineSec", DataFormat = DataFormat.TwosComplement)]
		public int offlineSec
		{
			get
			{
				return this._offlineSec;
			}
			set
			{
				this._offlineSec = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "lastOnlineDate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastOnlineDate
		{
			get
			{
				return this._lastOnlineDate;
			}
			set
			{
				this._lastOnlineDate = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "joinGuildUtc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int joinGuildUtc
		{
			get
			{
				return this._joinGuildUtc;
			}
			set
			{
				this._joinGuildUtc = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "appoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int appoint
		{
			get
			{
				return this._appoint;
			}
			set
			{
				this._appoint = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "contribution", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int contribution
		{
			get
			{
				return this._contribution;
			}
			set
			{
				this._contribution = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(13, IsRequired = false, Name = "vipLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vipLv
		{
			get
			{
				return this._vipLv;
			}
			set
			{
				this._vipLv = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "activity", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activity
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

		[ProtoMember(15, IsRequired = false, Name = "fund", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fund
		{
			get
			{
				return this._fund;
			}
			set
			{
				this._fund = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
