using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "CommonKV1")]
	[Serializable]
	public class CommonKV1 : IExtensible
	{
		private KVType.ENUM _ck;

		private int _cv;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "ck", DataFormat = DataFormat.TwosComplement)]
		public KVType.ENUM ck
		{
			get
			{
				return this._ck;
			}
			set
			{
				this._ck = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "cv", DataFormat = DataFormat.TwosComplement)]
		public int cv
		{
			get
			{
				return this._cv;
			}
			set
			{
				this._cv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
