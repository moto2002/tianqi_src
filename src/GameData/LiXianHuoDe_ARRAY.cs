using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "LiXianHuoDe_ARRAY")]
	[Serializable]
	public class LiXianHuoDe_ARRAY : IExtensible
	{
		private readonly List<LiXianHuoDe> _items = new List<LiXianHuoDe>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<LiXianHuoDe> items
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
