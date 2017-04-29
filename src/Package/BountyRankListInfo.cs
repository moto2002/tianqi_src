using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BountyRankListInfo")]
	[Serializable]
	public class BountyRankListInfo : IExtensible
	{
		private long _roleId;

		private string _roleName;

		private int _career;

		private int _rank;

		private int _score;

		private int _roleLv;

		private int _vipLv;

		private long _fighting;

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

		[ProtoMember(3, IsRequired = true, Name = "career", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = true, Name = "score", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(6, IsRequired = false, Name = "roleLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "vipLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "fighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
