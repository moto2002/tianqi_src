using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "BossBiaoQian_ARRAY")]
	[Serializable]
	public class BossBiaoQian_ARRAY : IExtensible
	{
		private readonly List<BossBiaoQian> _items = new List<BossBiaoQian>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<BossBiaoQian> items
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
