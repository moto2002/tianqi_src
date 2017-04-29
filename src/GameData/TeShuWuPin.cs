using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TeShuWuPin")]
	[Serializable]
	public class TeShuWuPin : IExtensible
	{
		private int _itemId;

		private int _effectId;

		private IExtension extensionObject;

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

		[ProtoMember(3, IsRequired = false, Name = "effectId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
