using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TuiJianZhuangBei_ARRAY")]
	[Serializable]
	public class TuiJianZhuangBei_ARRAY : IExtensible
	{
		private readonly List<TuiJianZhuangBei> _items = new List<TuiJianZhuangBei>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TuiJianZhuangBei> items
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
