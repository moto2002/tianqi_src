using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "BiaoZhunFenZhi_ARRAY")]
	[Serializable]
	public class BiaoZhunFenZhi_ARRAY : IExtensible
	{
		private readonly List<BiaoZhunFenZhi> _items = new List<BiaoZhunFenZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<BiaoZhunFenZhi> items
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
