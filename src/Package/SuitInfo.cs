using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "SuitInfo")]
	[Serializable]
	public class SuitInfo : IExtensible
	{
		private int _suitGroupId;

		private int _num;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "suitGroupId", DataFormat = DataFormat.TwosComplement)]
		public int suitGroupId
		{
			get
			{
				return this._suitGroupId;
			}
			set
			{
				this._suitGroupId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "num", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
