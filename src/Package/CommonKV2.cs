using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "CommonKV2")]
	[Serializable]
	public class CommonKV2 : IExtensible
	{
		private KVType.ENUM _ck;

		private string _cv;

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

		[ProtoMember(2, IsRequired = true, Name = "cv", DataFormat = DataFormat.Default)]
		public string cv
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
