using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1128), ForSend(1128), ProtoContract(Name = "BountyAccelerateBoxOpenReq")]
	[Serializable]
	public class BountyAccelerateBoxOpenReq : IExtensible
	{
		public static readonly short OP = 1128;

		private ulong _uId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "uId", DataFormat = DataFormat.TwosComplement)]
		public ulong uId
		{
			get
			{
				return this._uId;
			}
			set
			{
				this._uId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
