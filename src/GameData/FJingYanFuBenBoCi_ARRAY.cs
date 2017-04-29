using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FJingYanFuBenBoCi_ARRAY")]
	[Serializable]
	public class FJingYanFuBenBoCi_ARRAY : IExtensible
	{
		private readonly List<FJingYanFuBenBoCi> _items = new List<FJingYanFuBenBoCi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FJingYanFuBenBoCi> items
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
