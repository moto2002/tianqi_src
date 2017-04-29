using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JJingYingFuBenQuYu_ARRAY")]
	[Serializable]
	public class JJingYingFuBenQuYu_ARRAY : IExtensible
	{
		private readonly List<JJingYingFuBenQuYu> _items = new List<JJingYingFuBenQuYu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JJingYingFuBenQuYu> items
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
