using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuoChangJuQing_ARRAY")]
	[Serializable]
	public class GuoChangJuQing_ARRAY : IExtensible
	{
		private readonly List<GuoChangJuQing> _items = new List<GuoChangJuQing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuoChangJuQing> items
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
