using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "AcItemInfo")]
	[Serializable]
	public class AcItemInfo : IExtensible
	{
		[ProtoContract(Name = "awardItem")]
		[Serializable]
		public class awardItem : IExtensible
		{
			private int _itemCfgId;

			private int _itemNum;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "itemCfgId", DataFormat = DataFormat.TwosComplement)]
			public int itemCfgId
			{
				get
				{
					return this._itemCfgId;
				}
				set
				{
					this._itemCfgId = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "itemNum", DataFormat = DataFormat.TwosComplement)]
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

		private int _targetVal;

		private int _status;

		private readonly List<AcItemInfo.awardItem> _items = new List<AcItemInfo.awardItem>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "targetVal", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetVal
		{
			get
			{
				return this._targetVal;
			}
			set
			{
				this._targetVal = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "status", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(3, Name = "items", DataFormat = DataFormat.Default)]
		public List<AcItemInfo.awardItem> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
