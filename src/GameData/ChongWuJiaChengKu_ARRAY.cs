using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChongWuJiaChengKu_ARRAY")]
	[Serializable]
	public class ChongWuJiaChengKu_ARRAY : IExtensible
	{
		private readonly List<ChongWuJiaChengKu> _items = new List<ChongWuJiaChengKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChongWuJiaChengKu> items
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
