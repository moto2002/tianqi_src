using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "ArticleContent")]
	[Serializable]
	public class ArticleContent : IExtensible
	{
		private string _text;

		private readonly List<DetailInfo> _items = new List<DetailInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "text", DataFormat = DataFormat.Default)]
		public string text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		[ProtoMember(2, Name = "items", DataFormat = DataFormat.Default)]
		public List<DetailInfo> items
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
