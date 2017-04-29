using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanXinXi_ARRAY")]
	[Serializable]
	public class JunTuanZhanXinXi_ARRAY : IExtensible
	{
		private readonly List<JunTuanZhanXinXi> _items = new List<JunTuanZhanXinXi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JunTuanZhanXinXi> items
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
