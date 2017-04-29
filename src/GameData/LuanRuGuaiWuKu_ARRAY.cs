using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "LuanRuGuaiWuKu_ARRAY")]
	[Serializable]
	public class LuanRuGuaiWuKu_ARRAY : IExtensible
	{
		private readonly List<LuanRuGuaiWuKu> _items = new List<LuanRuGuaiWuKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<LuanRuGuaiWuKu> items
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
