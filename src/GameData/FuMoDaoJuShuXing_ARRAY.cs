using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FuMoDaoJuShuXing_ARRAY")]
	[Serializable]
	public class FuMoDaoJuShuXing_ARRAY : IExtensible
	{
		private readonly List<FuMoDaoJuShuXing> _items = new List<FuMoDaoJuShuXing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FuMoDaoJuShuXing> items
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
