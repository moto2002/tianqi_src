using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "HuoBi")]
	[Serializable]
	public class HuoBi : IExtensible
	{
		private int _money;

		private int _items;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "money", DataFormat = DataFormat.TwosComplement)]
		public int money
		{
			get
			{
				return this._money;
			}
			set
			{
				this._money = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "items", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int items
		{
			get
			{
				return this._items;
			}
			set
			{
				this._items = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
