using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JunTuanBOSSMoXing_ARRAY")]
	[Serializable]
	public class JunTuanBOSSMoXing_ARRAY : IExtensible
	{
		private readonly List<JunTuanBOSSMoXing> _items = new List<JunTuanBOSSMoXing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JunTuanBOSSMoXing> items
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
