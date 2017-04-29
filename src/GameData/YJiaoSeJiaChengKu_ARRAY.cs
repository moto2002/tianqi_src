using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YJiaoSeJiaChengKu_ARRAY")]
	[Serializable]
	public class YJiaoSeJiaChengKu_ARRAY : IExtensible
	{
		private readonly List<YJiaoSeJiaChengKu> _items = new List<YJiaoSeJiaChengKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YJiaoSeJiaChengKu> items
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
