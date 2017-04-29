using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "UIWidgetTable_ARRAY")]
	[Serializable]
	public class UIWidgetTable_ARRAY : IExtensible
	{
		private readonly List<UIWidgetTable> _items = new List<UIWidgetTable>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<UIWidgetTable> items
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
