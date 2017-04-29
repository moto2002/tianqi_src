using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "ShengCunMiJingPeiZhi")]
	[Serializable]
	public class ShengCunMiJingPeiZhi : IExtensible
	{
		private string _key;

		private int _value;

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

		[ProtoMember(3, IsRequired = true, Name = "value", DataFormat = DataFormat.TwosComplement)]
		public int value
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
