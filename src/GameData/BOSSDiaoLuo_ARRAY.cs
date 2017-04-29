using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "BOSSDiaoLuo_ARRAY")]
	[Serializable]
	public class BOSSDiaoLuo_ARRAY : IExtensible
	{
		private readonly List<BOSSDiaoLuo> _items = new List<BOSSDiaoLuo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<BOSSDiaoLuo> items
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
