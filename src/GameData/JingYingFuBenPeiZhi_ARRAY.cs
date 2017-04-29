using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JingYingFuBenPeiZhi_ARRAY")]
	[Serializable]
	public class JingYingFuBenPeiZhi_ARRAY : IExtensible
	{
		private readonly List<JingYingFuBenPeiZhi> _items = new List<JingYingFuBenPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JingYingFuBenPeiZhi> items
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
