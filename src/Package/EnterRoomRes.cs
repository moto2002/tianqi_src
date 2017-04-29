using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2764), ForSend(2764), ProtoContract(Name = "EnterRoomRes")]
	[Serializable]
	public class EnterRoomRes : IExtensible
	{
		public static readonly short OP = 2764;

		private int _deadlineTime = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "deadlineTime", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int deadlineTime
		{
			get
			{
				return this._deadlineTime;
			}
			set
			{
				this._deadlineTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
