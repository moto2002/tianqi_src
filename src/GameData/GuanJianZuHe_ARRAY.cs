using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuanJianZuHe_ARRAY")]
	[Serializable]
	public class GuanJianZuHe_ARRAY : IExtensible
	{
		private readonly List<GuanJianZuHe> _items = new List<GuanJianZuHe>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuanJianZuHe> items
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
