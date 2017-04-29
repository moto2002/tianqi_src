using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JiNengJieSuoBiao_ARRAY")]
	[Serializable]
	public class JiNengJieSuoBiao_ARRAY : IExtensible
	{
		private readonly List<JiNengJieSuoBiao> _items = new List<JiNengJieSuoBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JiNengJieSuoBiao> items
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
