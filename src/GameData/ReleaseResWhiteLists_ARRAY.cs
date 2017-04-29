using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ReleaseResWhiteLists_ARRAY")]
	[Serializable]
	public class ReleaseResWhiteLists_ARRAY : IExtensible
	{
		private readonly List<ReleaseResWhiteLists> _items = new List<ReleaseResWhiteLists>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ReleaseResWhiteLists> items
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
