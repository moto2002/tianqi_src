using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuaiWuKu_ARRAY")]
	[Serializable]
	public class GuaiWuKu_ARRAY : IExtensible
	{
		private readonly List<GuaiWuKu> _items = new List<GuaiWuKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuaiWuKu> items
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
