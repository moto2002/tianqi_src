using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JuQingZhiYinDuiHua_ARRAY")]
	[Serializable]
	public class JuQingZhiYinDuiHua_ARRAY : IExtensible
	{
		private readonly List<JuQingZhiYinDuiHua> _items = new List<JuQingZhiYinDuiHua>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JuQingZhiYinDuiHua> items
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
