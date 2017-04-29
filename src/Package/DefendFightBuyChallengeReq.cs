using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1070), ForSend(1070), ProtoContract(Name = "DefendFightBuyChallengeReq")]
	[Serializable]
	public class DefendFightBuyChallengeReq : IExtensible
	{
		public static readonly short OP = 1070;

		private DefendFightMode.DFMD _mode;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "mode", DataFormat = DataFormat.TwosComplement)]
		public DefendFightMode.DFMD mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
