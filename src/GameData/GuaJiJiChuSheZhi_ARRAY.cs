using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GuaJiJiChuSheZhi_ARRAY")]
	[Serializable]
	public class GuaJiJiChuSheZhi_ARRAY : IExtensible
	{
		private readonly List<GuaJiJiChuSheZhi> _items = new List<GuaJiJiChuSheZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GuaJiJiChuSheZhi> items
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
