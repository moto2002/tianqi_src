using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "EquipPartPeiZhi_ARRAY")]
	[Serializable]
	public class EquipPartPeiZhi_ARRAY : IExtensible
	{
		private readonly List<EquipPartPeiZhi> _items = new List<EquipPartPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<EquipPartPeiZhi> items
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
