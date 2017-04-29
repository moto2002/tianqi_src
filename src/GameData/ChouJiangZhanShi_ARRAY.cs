using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChouJiangZhanShi_ARRAY")]
	[Serializable]
	public class ChouJiangZhanShi_ARRAY : IExtensible
	{
		private readonly List<ChouJiangZhanShi> _items = new List<ChouJiangZhanShi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChouJiangZhanShi> items
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
