using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YChongWuJiaChengKu_ARRAY")]
	[Serializable]
	public class YChongWuJiaChengKu_ARRAY : IExtensible
	{
		private readonly List<YChongWuJiaChengKu> _items = new List<YChongWuJiaChengKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YChongWuJiaChengKu> items
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
