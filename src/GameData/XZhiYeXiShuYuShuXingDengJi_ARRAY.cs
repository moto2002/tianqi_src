using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XZhiYeXiShuYuShuXingDengJi_ARRAY")]
	[Serializable]
	public class XZhiYeXiShuYuShuXingDengJi_ARRAY : IExtensible
	{
		private readonly List<XZhiYeXiShuYuShuXingDengJi> _items = new List<XZhiYeXiShuYuShuXingDengJi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XZhiYeXiShuYuShuXingDengJi> items
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
