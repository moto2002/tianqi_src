using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DianFengZhanChangPeiZhi_ARRAY")]
	[Serializable]
	public class DianFengZhanChangPeiZhi_ARRAY : IExtensible
	{
		private readonly List<DianFengZhanChangPeiZhi> _items = new List<DianFengZhanChangPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DianFengZhanChangPeiZhi> items
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
