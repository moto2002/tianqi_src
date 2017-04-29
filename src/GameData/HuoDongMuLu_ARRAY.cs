using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "HuoDongMuLu_ARRAY")]
	[Serializable]
	public class HuoDongMuLu_ARRAY : IExtensible
	{
		private readonly List<HuoDongMuLu> _items = new List<HuoDongMuLu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<HuoDongMuLu> items
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
