using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "zZhuangBeiSheZhi_ARRAY")]
	[Serializable]
	public class zZhuangBeiSheZhi_ARRAY : IExtensible
	{
		private readonly List<zZhuangBeiSheZhi> _items = new List<zZhuangBeiSheZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<zZhuangBeiSheZhi> items
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
