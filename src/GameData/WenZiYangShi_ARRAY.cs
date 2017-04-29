using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "WenZiYangShi_ARRAY")]
	[Serializable]
	public class WenZiYangShi_ARRAY : IExtensible
	{
		private readonly List<WenZiYangShi> _items = new List<WenZiYangShi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<WenZiYangShi> items
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
