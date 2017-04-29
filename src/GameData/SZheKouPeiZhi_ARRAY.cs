using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "SZheKouPeiZhi_ARRAY")]
	[Serializable]
	public class SZheKouPeiZhi_ARRAY : IExtensible
	{
		private readonly List<SZheKouPeiZhi> _items = new List<SZheKouPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<SZheKouPeiZhi> items
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
