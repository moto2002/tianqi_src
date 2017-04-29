using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JJingYingFuBenPeiZhi_ARRAY")]
	[Serializable]
	public class JJingYingFuBenPeiZhi_ARRAY : IExtensible
	{
		private readonly List<JJingYingFuBenPeiZhi> _items = new List<JJingYingFuBenPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JJingYingFuBenPeiZhi> items
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
