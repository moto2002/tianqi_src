using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TiaoZhanFuBenPeiZhi_ARRAY")]
	[Serializable]
	public class TiaoZhanFuBenPeiZhi_ARRAY : IExtensible
	{
		private readonly List<TiaoZhanFuBenPeiZhi> _items = new List<TiaoZhanFuBenPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TiaoZhanFuBenPeiZhi> items
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
