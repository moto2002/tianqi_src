using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DianFengZhanChangPaiMingJiangLi_ARRAY")]
	[Serializable]
	public class DianFengZhanChangPaiMingJiangLi_ARRAY : IExtensible
	{
		private readonly List<DianFengZhanChangPaiMingJiangLi> _items = new List<DianFengZhanChangPaiMingJiangLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DianFengZhanChangPaiMingJiangLi> items
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
