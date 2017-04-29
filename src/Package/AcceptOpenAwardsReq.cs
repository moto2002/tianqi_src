using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(799), ForSend(799), ProtoContract(Name = "AcceptOpenAwardsReq")]
	[Serializable]
	public class AcceptOpenAwardsReq : IExtensible
	{
		public static readonly short OP = 799;

		private int _daySerial;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "daySerial", DataFormat = DataFormat.TwosComplement)]
		public int daySerial
		{
			get
			{
				return this._daySerial;
			}
			set
			{
				this._daySerial = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
