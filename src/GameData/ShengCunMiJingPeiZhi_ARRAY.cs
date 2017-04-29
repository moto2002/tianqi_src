using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShengCunMiJingPeiZhi_ARRAY")]
	[Serializable]
	public class ShengCunMiJingPeiZhi_ARRAY : IExtensible
	{
		private readonly List<ShengCunMiJingPeiZhi> _items = new List<ShengCunMiJingPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShengCunMiJingPeiZhi> items
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
