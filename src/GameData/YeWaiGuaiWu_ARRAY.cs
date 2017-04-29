using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YeWaiGuaiWu_ARRAY")]
	[Serializable]
	public class YeWaiGuaiWu_ARRAY : IExtensible
	{
		private readonly List<YeWaiGuaiWu> _items = new List<YeWaiGuaiWu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YeWaiGuaiWu> items
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
