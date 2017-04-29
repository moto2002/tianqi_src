using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YBanKuaiSuoYin_ARRAY")]
	[Serializable]
	public class YBanKuaiSuoYin_ARRAY : IExtensible
	{
		private readonly List<YBanKuaiSuoYin> _items = new List<YBanKuaiSuoYin>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YBanKuaiSuoYin> items
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
