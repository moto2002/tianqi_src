using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DuiHuaPeiZhi_ARRAY")]
	[Serializable]
	public class DuiHuaPeiZhi_ARRAY : IExtensible
	{
		private readonly List<DuiHuaPeiZhi> _items = new List<DuiHuaPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DuiHuaPeiZhi> items
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
