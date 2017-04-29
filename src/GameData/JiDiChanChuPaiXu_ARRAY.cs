using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JiDiChanChuPaiXu_ARRAY")]
	[Serializable]
	public class JiDiChanChuPaiXu_ARRAY : IExtensible
	{
		private readonly List<JiDiChanChuPaiXu> _items = new List<JiDiChanChuPaiXu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JiDiChanChuPaiXu> items
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
