using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuanLianXiTongRenWu_ARRAY")]
	[Serializable]
	public class GuanLianXiTongRenWu_ARRAY : IExtensible
	{
		private readonly List<GuanLianXiTongRenWu> _items = new List<GuanLianXiTongRenWu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuanLianXiTongRenWu> items
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
