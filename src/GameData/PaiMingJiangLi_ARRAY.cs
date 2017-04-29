using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "PaiMingJiangLi_ARRAY")]
	[Serializable]
	public class PaiMingJiangLi_ARRAY : IExtensible
	{
		private readonly List<PaiMingJiangLi> _items = new List<PaiMingJiangLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<PaiMingJiangLi> items
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
