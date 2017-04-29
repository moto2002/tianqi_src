using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JinJiShengLiBaoXiang_ARRAY")]
	[Serializable]
	public class JinJiShengLiBaoXiang_ARRAY : IExtensible
	{
		private readonly List<JinJiShengLiBaoXiang> _items = new List<JinJiShengLiBaoXiang>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JinJiShengLiBaoXiang> items
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
