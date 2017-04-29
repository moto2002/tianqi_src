using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ItemBriefInfo")]
	[Serializable]
	public class ItemBriefInfo : IExtensible
	{
		private int _cfgId;

		private long _count;

		private long _uId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "cfgId", DataFormat = DataFormat.TwosComplement)]
		public int cfgId
		{
			get
			{
				return this._cfgId;
			}
			set
			{
				this._cfgId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "count", DataFormat = DataFormat.TwosComplement)]
		public long count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "uId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long uId
		{
			get
			{
				return this._uId;
			}
			set
			{
				this._uId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
