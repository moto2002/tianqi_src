using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "AccumulateRechargeInfo")]
	[Serializable]
	public class AccumulateRechargeInfo : IExtensible
	{
		private int _id;

		private int _status;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "status", DataFormat = DataFormat.TwosComplement)]
		public int status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
