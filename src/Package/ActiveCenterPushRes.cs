using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(8832), ForSend(8832), ProtoContract(Name = "ActiveCenterPushRes")]
	[Serializable]
	public class ActiveCenterPushRes : IExtensible
	{
		public static readonly short OP = 8832;

		private long _serverTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "serverTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long serverTime
		{
			get
			{
				return this._serverTime;
			}
			set
			{
				this._serverTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
