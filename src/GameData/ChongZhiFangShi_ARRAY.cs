using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChongZhiFangShi_ARRAY")]
	[Serializable]
	public class ChongZhiFangShi_ARRAY : IExtensible
	{
		private readonly List<ChongZhiFangShi> _items = new List<ChongZhiFangShi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChongZhiFangShi> items
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
