using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuaJiDiTuPeiZhi_ARRAY")]
	[Serializable]
	public class GuaJiDiTuPeiZhi_ARRAY : IExtensible
	{
		private readonly List<GuaJiDiTuPeiZhi> _items = new List<GuaJiDiTuPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuaJiDiTuPeiZhi> items
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
