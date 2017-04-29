using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JiaZaiJieMianPeiTu_ARRAY")]
	[Serializable]
	public class JiaZaiJieMianPeiTu_ARRAY : IExtensible
	{
		private readonly List<JiaZaiJieMianPeiTu> _items = new List<JiaZaiJieMianPeiTu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JiaZaiJieMianPeiTu> items
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
