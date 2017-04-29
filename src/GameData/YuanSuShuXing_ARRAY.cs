using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YuanSuShuXing_ARRAY")]
	[Serializable]
	public class YuanSuShuXing_ARRAY : IExtensible
	{
		private readonly List<YuanSuShuXing> _items = new List<YuanSuShuXing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YuanSuShuXing> items
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
