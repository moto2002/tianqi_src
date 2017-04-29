using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DianFengZhanChangPeiZhi")]
	[Serializable]
	public class DianFengZhanChangPeiZhi : IExtensible
	{
		private string _key;

		private readonly List<int> _value = new List<int>();

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

		[ProtoMember(3, Name = "value", DataFormat = DataFormat.TwosComplement)]
		public List<int> value
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
