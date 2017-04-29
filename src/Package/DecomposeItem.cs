using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "DecomposeItem")]
	[Serializable]
	public class DecomposeItem : IExtensible
	{
		private int _itemId;

		private int _num;

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

		[ProtoMember(2, IsRequired = true, Name = "num", DataFormat = DataFormat.TwosComplement)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
