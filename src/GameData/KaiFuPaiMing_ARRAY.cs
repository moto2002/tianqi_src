using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "KaiFuPaiMing_ARRAY")]
	[Serializable]
	public class KaiFuPaiMing_ARRAY : IExtensible
	{
		private readonly List<KaiFuPaiMing> _items = new List<KaiFuPaiMing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<KaiFuPaiMing> items
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
