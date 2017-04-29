using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuangBoLianJie_ARRAY")]
	[Serializable]
	public class GuangBoLianJie_ARRAY : IExtensible
	{
		private readonly List<GuangBoLianJie> _items = new List<GuangBoLianJie>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuangBoLianJie> items
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
