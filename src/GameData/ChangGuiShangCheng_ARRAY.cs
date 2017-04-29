using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChangGuiShangCheng_ARRAY")]
	[Serializable]
	public class ChangGuiShangCheng_ARRAY : IExtensible
	{
		private readonly List<ChangGuiShangCheng> _items = new List<ChangGuiShangCheng>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChangGuiShangCheng> items
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
