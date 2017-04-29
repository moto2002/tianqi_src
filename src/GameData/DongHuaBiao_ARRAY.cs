using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DongHuaBiao_ARRAY")]
	[Serializable]
	public class DongHuaBiao_ARRAY : IExtensible
	{
		private readonly List<DongHuaBiao> _items = new List<DongHuaBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DongHuaBiao> items
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
