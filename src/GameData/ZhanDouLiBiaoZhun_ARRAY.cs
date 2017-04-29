using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhanDouLiBiaoZhun_ARRAY")]
	[Serializable]
	public class ZhanDouLiBiaoZhun_ARRAY : IExtensible
	{
		private readonly List<ZhanDouLiBiaoZhun> _items = new List<ZhanDouLiBiaoZhun>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhanDouLiBiaoZhun> items
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
