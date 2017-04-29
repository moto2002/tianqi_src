using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TongGuanDiaoLuo_ARRAY")]
	[Serializable]
	public class TongGuanDiaoLuo_ARRAY : IExtensible
	{
		private readonly List<TongGuanDiaoLuo> _items = new List<TongGuanDiaoLuo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TongGuanDiaoLuo> items
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
