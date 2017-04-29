using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JuQingZhiYinBuZou_ARRAY")]
	[Serializable]
	public class JuQingZhiYinBuZou_ARRAY : IExtensible
	{
		private readonly List<JuQingZhiYinBuZou> _items = new List<JuQingZhiYinBuZou>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JuQingZhiYinBuZou> items
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
