using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChongWuRenWuPeiZhi")]
	[Serializable]
	public class ChongWuRenWuPeiZhi : IExtensible
	{
		private string _key;

		private readonly List<string> _value = new List<string>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "key", DataFormat = DataFormat.Default)]
		public string key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		[ProtoMember(3, Name = "value", DataFormat = DataFormat.Default)]
		public List<string> value
		{
			get
			{
				return this._value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
