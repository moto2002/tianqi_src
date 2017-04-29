using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DuiWuMuBiao_ARRAY")]
	[Serializable]
	public class DuiWuMuBiao_ARRAY : IExtensible
	{
		private readonly List<DuiWuMuBiao> _items = new List<DuiWuMuBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DuiWuMuBiao> items
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
