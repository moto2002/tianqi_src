using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShengCunMiJingCiShuGouMai_ARRAY")]
	[Serializable]
	public class ShengCunMiJingCiShuGouMai_ARRAY : IExtensible
	{
		private readonly List<ShengCunMiJingCiShuGouMai> _items = new List<ShengCunMiJingCiShuGouMai>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShengCunMiJingCiShuGouMai> items
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
