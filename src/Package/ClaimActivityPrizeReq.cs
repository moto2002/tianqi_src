using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(930), ForSend(930), ProtoContract(Name = "ClaimActivityPrizeReq")]
	[Serializable]
	public class ClaimActivityPrizeReq : IExtensible
	{
		public static readonly short OP = 930;

		private int _activity;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "activity", DataFormat = DataFormat.TwosComplement)]
		public int activity
		{
			get
			{
				return this._activity;
			}
			set
			{
				this._activity = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
