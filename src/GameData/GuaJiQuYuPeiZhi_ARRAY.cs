using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuaJiQuYuPeiZhi_ARRAY")]
	[Serializable]
	public class GuaJiQuYuPeiZhi_ARRAY : IExtensible
	{
		private readonly List<GuaJiQuYuPeiZhi> _items = new List<GuaJiQuYuPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuaJiQuYuPeiZhi> items
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
