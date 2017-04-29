using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "AdvancedCondition_ARRAY")]
	[Serializable]
	public class AdvancedCondition_ARRAY : IExtensible
	{
		private readonly List<AdvancedCondition> _items = new List<AdvancedCondition>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<AdvancedCondition> items
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
