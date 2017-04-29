using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "KaiFuHuoDong_ARRAY")]
	[Serializable]
	public class KaiFuHuoDong_ARRAY : IExtensible
	{
		private readonly List<KaiFuHuoDong> _items = new List<KaiFuHuoDong>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<KaiFuHuoDong> items
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
