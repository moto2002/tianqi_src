using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuanLiGongNeng_ARRAY")]
	[Serializable]
	public class GuanLiGongNeng_ARRAY : IExtensible
	{
		private readonly List<GuanLiGongNeng> _items = new List<GuanLiGongNeng>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuanLiGongNeng> items
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
