using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "LiXianJieMianPeiZhi_ARRAY")]
	[Serializable]
	public class LiXianJieMianPeiZhi_ARRAY : IExtensible
	{
		private readonly List<LiXianJieMianPeiZhi> _items = new List<LiXianJieMianPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<LiXianJieMianPeiZhi> items
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
