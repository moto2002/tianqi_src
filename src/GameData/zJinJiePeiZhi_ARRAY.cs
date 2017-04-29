using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "zJinJiePeiZhi_ARRAY")]
	[Serializable]
	public class zJinJiePeiZhi_ARRAY : IExtensible
	{
		private readonly List<zJinJiePeiZhi> _items = new List<zJinJiePeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<zJinJiePeiZhi> items
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
