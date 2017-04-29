using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "PVPMeiRiJiangLi_ARRAY")]
	[Serializable]
	public class PVPMeiRiJiangLi_ARRAY : IExtensible
	{
		private readonly List<PVPMeiRiJiangLi> _items = new List<PVPMeiRiJiangLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<PVPMeiRiJiangLi> items
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
