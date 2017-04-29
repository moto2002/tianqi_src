using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(493), ForSend(493), ProtoContract(Name = "TimeSyncNty")]
	[Serializable]
	public class TimeSyncNty : IExtensible
	{
		public static readonly short OP = 493;

		private int _sequenceNo;

		private long _serverTime = 1L;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "sequenceNo", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sequenceNo
		{
			get
			{
				return this._sequenceNo;
			}
			set
			{
				this._sequenceNo = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "serverTime", DataFormat = DataFormat.TwosComplement), DefaultValue(1L)]
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
