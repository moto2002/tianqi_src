using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DangQianDengJiLiLunZhanLi_ARRAY")]
	[Serializable]
	public class DangQianDengJiLiLunZhanLi_ARRAY : IExtensible
	{
		private readonly List<DangQianDengJiLiLunZhanLi> _items = new List<DangQianDengJiLiLunZhanLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DangQianDengJiLiLunZhanLi> items
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
