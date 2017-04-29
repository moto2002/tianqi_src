using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShouChongSongLi")]
	[Serializable]
	public class ShouChongSongLi : IExtensible
	{
		private int _itemDropId;

		private readonly List<int> _isEffect = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "itemDropId", DataFormat = DataFormat.TwosComplement)]
		public int itemDropId
		{
			get
			{
				return this._itemDropId;
			}
			set
			{
				this._itemDropId = value;
			}
		}

		[ProtoMember(3, Name = "isEffect", DataFormat = DataFormat.TwosComplement)]
		public List<int> isEffect
		{
			get
			{
				return this._isEffect;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
