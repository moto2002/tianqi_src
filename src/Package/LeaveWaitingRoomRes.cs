using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(826), ForSend(826), ProtoContract(Name = "LeaveWaitingRoomRes")]
	[Serializable]
	public class LeaveWaitingRoomRes : IExtensible
	{
		public static readonly short OP = 826;

		private long _hp = -1L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "hp", DataFormat = DataFormat.TwosComplement), DefaultValue(-1L)]
		public long hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this._hp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
