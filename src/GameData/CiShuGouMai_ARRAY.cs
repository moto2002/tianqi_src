using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "CiShuGouMai_ARRAY")]
	[Serializable]
	public class CiShuGouMai_ARRAY : IExtensible
	{
		private readonly List<CiShuGouMai> _items = new List<CiShuGouMai>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<CiShuGouMai> items
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
