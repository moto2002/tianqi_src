using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GongHuiRiZhi_ARRAY")]
	[Serializable]
	public class GongHuiRiZhi_ARRAY : IExtensible
	{
		private readonly List<GongHuiRiZhi> _items = new List<GongHuiRiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GongHuiRiZhi> items
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
