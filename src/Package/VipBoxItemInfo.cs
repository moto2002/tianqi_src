using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "VipBoxItemInfo")]
	[Serializable]
	public class VipBoxItemInfo : IExtensible
	{
		private int _effectId;

		private int _itemId;

		private int _itemCount;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "effectId", DataFormat = DataFormat.TwosComplement)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "itemCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemCount
		{
			get
			{
				return this._itemCount;
			}
			set
			{
				this._itemCount = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
