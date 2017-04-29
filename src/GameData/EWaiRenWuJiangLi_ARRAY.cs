using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "EWaiRenWuJiangLi_ARRAY")]
	[Serializable]
	public class EWaiRenWuJiangLi_ARRAY : IExtensible
	{
		private readonly List<EWaiRenWuJiangLi> _items = new List<EWaiRenWuJiangLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<EWaiRenWuJiangLi> items
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
