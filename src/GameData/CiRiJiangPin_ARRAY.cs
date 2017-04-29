using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "CiRiJiangPin_ARRAY")]
	[Serializable]
	public class CiRiJiangPin_ARRAY : IExtensible
	{
		private readonly List<CiRiJiangPin> _items = new List<CiRiJiangPin>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<CiRiJiangPin> items
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
