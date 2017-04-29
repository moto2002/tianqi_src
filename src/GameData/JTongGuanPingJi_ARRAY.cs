using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JTongGuanPingJi_ARRAY")]
	[Serializable]
	public class JTongGuanPingJi_ARRAY : IExtensible
	{
		private readonly List<JTongGuanPingJi> _items = new List<JTongGuanPingJi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JTongGuanPingJi> items
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
