using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YKuangJingKu_ARRAY")]
	[Serializable]
	public class YKuangJingKu_ARRAY : IExtensible
	{
		private readonly List<YKuangJingKu> _items = new List<YKuangJingKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YKuangJingKu> items
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
