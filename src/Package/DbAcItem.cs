using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DbAcItem")]
	[Serializable]
	public class DbAcItem : IExtensible
	{
		private int _targetVal;

		private int _status;

		private readonly List<ItemBriefInfo> _items = new List<ItemBriefInfo>();

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
		public List<ItemBriefInfo> items
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
