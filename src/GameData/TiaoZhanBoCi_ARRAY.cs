using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TiaoZhanBoCi_ARRAY")]
	[Serializable]
	public class TiaoZhanBoCi_ARRAY : IExtensible
	{
		private readonly List<TiaoZhanBoCi> _items = new List<TiaoZhanBoCi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TiaoZhanBoCi> items
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
