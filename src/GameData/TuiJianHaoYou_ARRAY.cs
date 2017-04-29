using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TuiJianHaoYou_ARRAY")]
	[Serializable]
	public class TuiJianHaoYou_ARRAY : IExtensible
	{
		private readonly List<TuiJianHaoYou> _items = new List<TuiJianHaoYou>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TuiJianHaoYou> items
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
