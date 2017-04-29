using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "SuiJiGuaiWuKu_ARRAY")]
	[Serializable]
	public class SuiJiGuaiWuKu_ARRAY : IExtensible
	{
		private readonly List<SuiJiGuaiWuKu> _items = new List<SuiJiGuaiWuKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<SuiJiGuaiWuKu> items
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
