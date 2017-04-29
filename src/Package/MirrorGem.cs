using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "MirrorGem")]
	[Serializable]
	public class MirrorGem : IExtensible
	{
		private int _type;

		private readonly List<KeyAndValue> _keyAndValues = new List<KeyAndValue>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, Name = "keyAndValues", DataFormat = DataFormat.Default)]
		public List<KeyAndValue> keyAndValues
		{
			get
			{
				return this._keyAndValues;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
