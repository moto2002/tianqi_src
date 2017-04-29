using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FShouHuShuiJingFuBenBoCi_ARRAY")]
	[Serializable]
	public class FShouHuShuiJingFuBenBoCi_ARRAY : IExtensible
	{
		private readonly List<FShouHuShuiJingFuBenBoCi> _items = new List<FShouHuShuiJingFuBenBoCi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FShouHuShuiJingFuBenBoCi> items
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
