using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "CopyReward")]
	[Serializable]
	public class CopyReward : IExtensible
	{
		private int _itemId;

		private int _itemNum;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "itemNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemNum
		{
			get
			{
				return this._itemNum;
			}
			set
			{
				this._itemNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
