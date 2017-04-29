using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XiTongKaiQiYuGao_ARRAY")]
	[Serializable]
	public class XiTongKaiQiYuGao_ARRAY : IExtensible
	{
		private readonly List<XiTongKaiQiYuGao> _items = new List<XiTongKaiQiYuGao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XiTongKaiQiYuGao> items
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
