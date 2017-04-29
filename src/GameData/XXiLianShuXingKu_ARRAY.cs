using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XXiLianShuXingKu_ARRAY")]
	[Serializable]
	public class XXiLianShuXingKu_ARRAY : IExtensible
	{
		private readonly List<XXiLianShuXingKu> _items = new List<XXiLianShuXingKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XXiLianShuXingKu> items
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
