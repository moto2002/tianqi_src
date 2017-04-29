using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YWanFaSheZhi_ARRAY")]
	[Serializable]
	public class YWanFaSheZhi_ARRAY : IExtensible
	{
		private readonly List<YWanFaSheZhi> _items = new List<YWanFaSheZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YWanFaSheZhi> items
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
