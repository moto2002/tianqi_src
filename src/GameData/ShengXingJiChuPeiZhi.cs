using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShengXingJiChuPeiZhi")]
	[Serializable]
	public class ShengXingJiChuPeiZhi : IExtensible
	{
		private string _key;

		private int _num;

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

		[ProtoMember(3, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(4, Name = "value", DataFormat = DataFormat.TwosComplement)]
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
