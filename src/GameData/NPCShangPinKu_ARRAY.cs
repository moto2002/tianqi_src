using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "NPCShangPinKu_ARRAY")]
	[Serializable]
	public class NPCShangPinKu_ARRAY : IExtensible
	{
		private readonly List<NPCShangPinKu> _items = new List<NPCShangPinKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<NPCShangPinKu> items
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
