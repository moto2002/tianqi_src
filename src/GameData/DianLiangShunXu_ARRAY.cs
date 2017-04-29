using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DianLiangShunXu_ARRAY")]
	[Serializable]
	public class DianLiangShunXu_ARRAY : IExtensible
	{
		private readonly List<DianLiangShunXu> _items = new List<DianLiangShunXu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DianLiangShunXu> items
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
