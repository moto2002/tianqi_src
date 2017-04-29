using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JiNengShengJiBiao_ARRAY")]
	[Serializable]
	public class JiNengShengJiBiao_ARRAY : IExtensible
	{
		private readonly List<JiNengShengJiBiao> _items = new List<JiNengShengJiBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JiNengShengJiBiao> items
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
