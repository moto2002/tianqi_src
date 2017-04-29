using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "PaoHuanRenWuPeiZhi")]
	[Serializable]
	public class PaoHuanRenWuPeiZhi : IExtensible
	{
		private string _key;

		private string _value;

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

		[ProtoMember(3, IsRequired = true, Name = "value", DataFormat = DataFormat.Default)]
		public string value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
