using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TaoZhuangGaoDiJiGuanXi_ARRAY")]
	[Serializable]
	public class TaoZhuangGaoDiJiGuanXi_ARRAY : IExtensible
	{
		private readonly List<TaoZhuangGaoDiJiGuanXi> _items = new List<TaoZhuangGaoDiJiGuanXi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TaoZhuangGaoDiJiGuanXi> items
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
