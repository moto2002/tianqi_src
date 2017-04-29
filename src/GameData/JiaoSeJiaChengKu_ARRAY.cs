using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JiaoSeJiaChengKu_ARRAY")]
	[Serializable]
	public class JiaoSeJiaChengKu_ARRAY : IExtensible
	{
		private readonly List<JiaoSeJiaChengKu> _items = new List<JiaoSeJiaChengKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JiaoSeJiaChengKu> items
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
