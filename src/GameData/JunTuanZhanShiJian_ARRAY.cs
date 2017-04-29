using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanShiJian_ARRAY")]
	[Serializable]
	public class JunTuanZhanShiJian_ARRAY : IExtensible
	{
		private readonly List<JunTuanZhanShiJian> _items = new List<JunTuanZhanShiJian>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JunTuanZhanShiJian> items
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
