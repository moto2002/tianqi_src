using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JunTuanZhanCaiJi_ARRAY")]
	[Serializable]
	public class JunTuanZhanCaiJi_ARRAY : IExtensible
	{
		private readonly List<JunTuanZhanCaiJi> _items = new List<JunTuanZhanCaiJi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JunTuanZhanCaiJi> items
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
