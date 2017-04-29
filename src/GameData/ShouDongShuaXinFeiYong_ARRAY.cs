using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShouDongShuaXinFeiYong_ARRAY")]
	[Serializable]
	public class ShouDongShuaXinFeiYong_ARRAY : IExtensible
	{
		private readonly List<ShouDongShuaXinFeiYong> _items = new List<ShouDongShuaXinFeiYong>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShouDongShuaXinFeiYong> items
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
