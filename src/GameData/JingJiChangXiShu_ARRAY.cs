using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JingJiChangXiShu_ARRAY")]
	[Serializable]
	public class JingJiChangXiShu_ARRAY : IExtensible
	{
		private readonly List<JingJiChangXiShu> _items = new List<JingJiChangXiShu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JingJiChangXiShu> items
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
