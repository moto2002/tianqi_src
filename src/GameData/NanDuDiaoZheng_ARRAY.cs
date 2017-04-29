using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "NanDuDiaoZheng_ARRAY")]
	[Serializable]
	public class NanDuDiaoZheng_ARRAY : IExtensible
	{
		private readonly List<NanDuDiaoZheng> _items = new List<NanDuDiaoZheng>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<NanDuDiaoZheng> items
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
