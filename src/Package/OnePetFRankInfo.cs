using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "OnePetFRankInfo")]
	[Serializable]
	public class OnePetFRankInfo : IExtensible
	{
		private int _petF;

		private long _petCfgId;

		private int _quality;

		private int _star;

		private int _rank;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "petF", DataFormat = DataFormat.TwosComplement)]
		public int petF
		{
			get
			{
				return this._petF;
			}
			set
			{
				this._petF = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "petCfgId", DataFormat = DataFormat.TwosComplement)]
		public long petCfgId
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

		[ProtoMember(3, IsRequired = true, Name = "quality", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = true, Name = "star", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = true, Name = "rank", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
