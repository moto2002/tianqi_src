using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(980), ForSend(980), ProtoContract(Name = "ClientDrvBattleDeathNty")]
	[Serializable]
	public class ClientDrvBattleDeathNty : IExtensible
	{
		public static readonly short OP = 980;

		private long _soldierId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "soldierId", DataFormat = DataFormat.TwosComplement)]
		public long soldierId
		{
			get
			{
				return this._soldierId;
			}
			set
			{
				this._soldierId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
