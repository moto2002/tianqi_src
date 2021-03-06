using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JinJiKaiFangShiJian_ARRAY")]
	[Serializable]
	public class JinJiKaiFangShiJian_ARRAY : IExtensible
	{
		private readonly List<JinJiKaiFangShiJian> _items = new List<JinJiKaiFangShiJian>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JinJiKaiFangShiJian> items
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
