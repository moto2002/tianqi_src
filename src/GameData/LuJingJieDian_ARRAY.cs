using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "LuJingJieDian_ARRAY")]
	[Serializable]
	public class LuJingJieDian_ARRAY : IExtensible
	{
		private readonly List<LuJingJieDian> _items = new List<LuJingJieDian>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<LuJingJieDian> items
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
