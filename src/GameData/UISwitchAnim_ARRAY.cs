using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "UISwitchAnim_ARRAY")]
	[Serializable]
	public class UISwitchAnim_ARRAY : IExtensible
	{
		private readonly List<UISwitchAnim> _items = new List<UISwitchAnim>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<UISwitchAnim> items
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
