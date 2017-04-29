using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChangJingSuiJiKu_ARRAY")]
	[Serializable]
	public class ChangJingSuiJiKu_ARRAY : IExtensible
	{
		private readonly List<ChangJingSuiJiKu> _items = new List<ChangJingSuiJiKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChangJingSuiJiKu> items
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
