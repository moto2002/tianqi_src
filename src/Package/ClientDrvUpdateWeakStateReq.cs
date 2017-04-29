using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3804), ForSend(3804), ProtoContract(Name = "ClientDrvUpdateWeakStateReq")]
	[Serializable]
	public class ClientDrvUpdateWeakStateReq : IExtensible
	{
		public static readonly short OP = 3804;

		private long _soldierId;

		private int _state;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "soldierId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "state", DataFormat = DataFormat.TwosComplement)]
		public int state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
