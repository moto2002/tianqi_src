using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(884), ForSend(884), ProtoContract(Name = "BountyTaskOpenBoxReq")]
	[Serializable]
	public class BountyTaskOpenBoxReq : IExtensible
	{
		public static readonly short OP = 884;

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
