using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "BanKuaiSuoYin_ARRAY")]
	[Serializable]
	public class BanKuaiSuoYin_ARRAY : IExtensible
	{
		private readonly List<BanKuaiSuoYin> _items = new List<BanKuaiSuoYin>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<BanKuaiSuoYin> items
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
