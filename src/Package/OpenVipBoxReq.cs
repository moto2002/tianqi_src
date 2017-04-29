using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(551), ForSend(551), ProtoContract(Name = "OpenVipBoxReq")]
	[Serializable]
	public class OpenVipBoxReq : IExtensible
	{
		public static readonly short OP = 551;

		private int _effectId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "effectId", DataFormat = DataFormat.TwosComplement)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
