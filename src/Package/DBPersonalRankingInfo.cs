using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DBPersonalRankingInfo")]
	[Serializable]
	public class DBPersonalRankingInfo : IExtensible
	{
		private long _roleId;

		private string _name;

		private int _lv;

		private long _fighting;

		private long _exp;

		private int _career;

		private readonly List<long> _petId = new List<long>();

		private int _uLv;

		private long _uFighting;

		private int _uExp;

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

		[ProtoMember(4, IsRequired = true, Name = "fighting", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = true, Name = "exp", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(6, IsRequired = true, Name = "career", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(7, Name = "petId", DataFormat = DataFormat.TwosComplement)]
		public List<long> petId
		{
			get
			{
				return this._petId;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "uLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int uLv
		{
			get
			{
				return this._uLv;
			}
			set
			{
				this._uLv = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "uFighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long uFighting
		{
			get
			{
				return this._uFighting;
			}
			set
			{
				this._uFighting = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "uExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int uExp
		{
			get
			{
				return this._uExp;
			}
			set
			{
				this._uExp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
