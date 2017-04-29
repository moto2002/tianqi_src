using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TiaoZhanGuanQia_ARRAY")]
	[Serializable]
	public class TiaoZhanGuanQia_ARRAY : IExtensible
	{
		private readonly List<TiaoZhanGuanQia> _items = new List<TiaoZhanGuanQia>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TiaoZhanGuanQia> items
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
