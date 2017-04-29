using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YuanSuJiNeng_ARRAY")]
	[Serializable]
	public class YuanSuJiNeng_ARRAY : IExtensible
	{
		private readonly List<YuanSuJiNeng> _items = new List<YuanSuJiNeng>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YuanSuJiNeng> items
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
