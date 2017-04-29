using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "PinZhiYanSe_ARRAY")]
	[Serializable]
	public class PinZhiYanSe_ARRAY : IExtensible
	{
		private readonly List<PinZhiYanSe> _items = new List<PinZhiYanSe>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<PinZhiYanSe> items
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
