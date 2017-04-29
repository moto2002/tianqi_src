using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "OneLvRankInfo")]
	[Serializable]
	public class OneLvRankInfo : IExtensible
	{
		private int _lv;

		private int _rank;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
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
