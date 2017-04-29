using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DBPetRankingInfo")]
	[Serializable]
	public class DBPetRankingInfo : IExtensible
	{
		private long _roleId;

		private string _name;

		private long _petId;

		private int _petCfgId;

		private long _fighting;

		private int _quality;

		private int _star;

		private int _rank;

		private long _uFighting;

		private int _uQuality;

		private int _uStar;

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

		[ProtoMember(3, IsRequired = true, Name = "petId", DataFormat = DataFormat.TwosComplement)]
		public long petId
		{
			get
			{
				return this._petId;
			}
			set
			{
				this._petId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "petCfgId", DataFormat = DataFormat.TwosComplement)]
		public int petCfgId
		{
			get
			{
				return this._petCfgId;
			}
			set
			{
				this._petCfgId = value;
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

		[ProtoMember(6, IsRequired = true, Name = "quality", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(7, IsRequired = true, Name = "star", DataFormat = DataFormat.TwosComplement)]
		public int star
		{
			get
			{
				return this._star;
			}
			set
			{
				this._star = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "rank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "uQuality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int uQuality
		{
			get
			{
				return this._uQuality;
			}
			set
			{
				this._uQuality = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "uStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int uStar
		{
			get
			{
				return this._uStar;
			}
			set
			{
				this._uStar = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
