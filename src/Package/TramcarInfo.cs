using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "TramcarInfo")]
	[Serializable]
	public class TramcarInfo : IExtensible
	{
		private int _quality;

		private readonly List<DropItem> _item = new List<DropItem>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "quality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this._quality = value;
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
