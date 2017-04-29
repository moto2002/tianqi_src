using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanLianShengJiangLi_ARRAY")]
	[Serializable]
	public class JunTuanZhanLianShengJiangLi_ARRAY : IExtensible
	{
		private readonly List<JunTuanZhanLianShengJiangLi> _items = new List<JunTuanZhanLianShengJiangLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JunTuanZhanLianShengJiangLi> items
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
