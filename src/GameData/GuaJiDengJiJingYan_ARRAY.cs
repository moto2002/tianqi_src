using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuaJiDengJiJingYan_ARRAY")]
	[Serializable]
	public class GuaJiDengJiJingYan_ARRAY : IExtensible
	{
		private readonly List<GuaJiDengJiJingYan> _items = new List<GuaJiDengJiJingYan>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuaJiDengJiJingYan> items
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
