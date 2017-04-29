using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GouMaiJiaGe_ARRAY")]
	[Serializable]
	public class GouMaiJiaGe_ARRAY : IExtensible
	{
		private readonly List<GouMaiJiaGe> _items = new List<GouMaiJiaGe>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GouMaiJiaGe> items
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
