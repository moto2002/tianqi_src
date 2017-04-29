using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "KaiFuKuangHuan_ARRAY")]
	[Serializable]
	public class KaiFuKuangHuan_ARRAY : IExtensible
	{
		private readonly List<KaiFuKuangHuan> _items = new List<KaiFuKuangHuan>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<KaiFuKuangHuan> items
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
