using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "SShangChengLeiXing_ARRAY")]
	[Serializable]
	public class SShangChengLeiXing_ARRAY : IExtensible
	{
		private readonly List<SShangChengLeiXing> _items = new List<SShangChengLeiXing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<SShangChengLeiXing> items
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
