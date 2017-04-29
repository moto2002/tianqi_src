using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "OneFightingRankInfo")]
	[Serializable]
	public class OneFightingRankInfo : IExtensible
	{
		private long _fighting;

		private int _rank;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "fighting", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "rank", DataFormat = DataFormat.TwosComplement)]
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
