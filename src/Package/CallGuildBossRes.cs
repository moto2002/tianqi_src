using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(69), ForSend(69), ProtoContract(Name = "CallGuildBossRes")]
	[Serializable]
	public class CallGuildBossRes : IExtensible
	{
		public static readonly short OP = 69;

		private int _bossId;

		private int _canCallTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "bossId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossId
		{
			get
			{
				return this._bossId;
			}
			set
			{
				this._bossId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "canCallTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int canCallTimes
		{
			get
			{
				return this._canCallTimes;
			}
			set
			{
				this._canCallTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
