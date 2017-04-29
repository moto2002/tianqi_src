using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XiLianJiChuSheZhi_ARRAY")]
	[Serializable]
	public class XiLianJiChuSheZhi_ARRAY : IExtensible
	{
		private readonly List<XiLianJiChuSheZhi> _items = new List<XiLianJiChuSheZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XiLianJiChuSheZhi> items
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
