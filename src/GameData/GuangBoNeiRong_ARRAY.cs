using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuangBoNeiRong_ARRAY")]
	[Serializable]
	public class GuangBoNeiRong_ARRAY : IExtensible
	{
		private readonly List<GuangBoNeiRong> _items = new List<GuangBoNeiRong>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuangBoNeiRong> items
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
