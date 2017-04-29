using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JieMianTeXiao_ARRAY")]
	[Serializable]
	public class JieMianTeXiao_ARRAY : IExtensible
	{
		private readonly List<JieMianTeXiao> _items = new List<JieMianTeXiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JieMianTeXiao> items
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
