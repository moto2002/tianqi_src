using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "SShenBingPeiZhi_ARRAY")]
	[Serializable]
	public class SShenBingPeiZhi_ARRAY : IExtensible
	{
		private readonly List<SShenBingPeiZhi> _items = new List<SShenBingPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<SShenBingPeiZhi> items
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
