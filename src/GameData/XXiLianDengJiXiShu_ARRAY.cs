using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XXiLianDengJiXiShu_ARRAY")]
	[Serializable]
	public class XXiLianDengJiXiShu_ARRAY : IExtensible
	{
		private readonly List<XXiLianDengJiXiShu> _items = new List<XXiLianDengJiXiShu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XXiLianDengJiXiShu> items
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
