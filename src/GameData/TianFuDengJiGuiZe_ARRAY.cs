using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TianFuDengJiGuiZe_ARRAY")]
	[Serializable]
	public class TianFuDengJiGuiZe_ARRAY : IExtensible
	{
		private readonly List<TianFuDengJiGuiZe> _items = new List<TianFuDengJiGuiZe>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TianFuDengJiGuiZe> items
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
