using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DuoRenDuiZhanPiPeiGuiZe_ARRAY")]
	[Serializable]
	public class DuoRenDuiZhanPiPeiGuiZe_ARRAY : IExtensible
	{
		private readonly List<DuoRenDuiZhanPiPeiGuiZe> _items = new List<DuoRenDuiZhanPiPeiGuiZe>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DuoRenDuiZhanPiPeiGuiZe> items
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
