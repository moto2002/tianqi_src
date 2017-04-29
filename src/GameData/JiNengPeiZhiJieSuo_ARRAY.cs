using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JiNengPeiZhiJieSuo_ARRAY")]
	[Serializable]
	public class JiNengPeiZhiJieSuo_ARRAY : IExtensible
	{
		private readonly List<JiNengPeiZhiJieSuo> _items = new List<JiNengPeiZhiJieSuo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JiNengPeiZhiJieSuo> items
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
