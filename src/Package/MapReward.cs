using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "MapReward")]
	[Serializable]
	public class MapReward : IExtensible
	{
		private int _mapId;

		private readonly List<DropItem> _item = new List<DropItem>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "mapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mapId
		{
			get
			{
				return this._mapId;
			}
			set
			{
				this._mapId = value;
			}
		}

		[ProtoMember(2, Name = "item", DataFormat = DataFormat.Default)]
		public List<DropItem> item
		{
			get
			{
				return this._item;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
