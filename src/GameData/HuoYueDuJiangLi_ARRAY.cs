using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "HuoYueDuJiangLi_ARRAY")]
	[Serializable]
	public class HuoYueDuJiangLi_ARRAY : IExtensible
	{
		private readonly List<HuoYueDuJiangLi> _items = new List<HuoYueDuJiangLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<HuoYueDuJiangLi> items
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
