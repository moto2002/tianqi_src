using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YNengLiangHuiFu_ARRAY")]
	[Serializable]
	public class YNengLiangHuiFu_ARRAY : IExtensible
	{
		private readonly List<YNengLiangHuiFu> _items = new List<YNengLiangHuiFu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YNengLiangHuiFu> items
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
