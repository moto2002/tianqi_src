using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "BUFFXiaoHao_ARRAY")]
	[Serializable]
	public class BUFFXiaoHao_ARRAY : IExtensible
	{
		private readonly List<BUFFXiaoHao> _items = new List<BUFFXiaoHao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<BUFFXiaoHao> items
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
