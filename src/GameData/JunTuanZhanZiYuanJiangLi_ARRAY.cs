using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanZiYuanJiangLi_ARRAY")]
	[Serializable]
	public class JunTuanZhanZiYuanJiangLi_ARRAY : IExtensible
	{
		private readonly List<JunTuanZhanZiYuanJiangLi> _items = new List<JunTuanZhanZiYuanJiangLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JunTuanZhanZiYuanJiangLi> items
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
