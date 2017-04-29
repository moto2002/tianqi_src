using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XuanShangSheZhi_ARRAY")]
	[Serializable]
	public class XuanShangSheZhi_ARRAY : IExtensible
	{
		private readonly List<XuanShangSheZhi> _items = new List<XuanShangSheZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XuanShangSheZhi> items
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
