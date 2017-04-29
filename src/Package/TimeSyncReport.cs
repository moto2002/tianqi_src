using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(637), ForSend(637), ProtoContract(Name = "TimeSyncReport")]
	[Serializable]
	public class TimeSyncReport : IExtensible
	{
		public static readonly short OP = 637;

		private int _sequenceNo;

		private long _clientTime = 1L;

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

		[ProtoMember(2, IsRequired = false, Name = "clientTime", DataFormat = DataFormat.TwosComplement), DefaultValue(1L)]
		public long clientTime
		{
			get
			{
				return this._clientTime;
			}
			set
			{
				this._clientTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
