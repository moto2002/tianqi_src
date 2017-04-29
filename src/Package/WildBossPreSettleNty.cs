using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(226), ForSend(226), ProtoContract(Name = "WildBossPreSettleNty")]
	[Serializable]
	public class WildBossPreSettleNty : IExtensible
	{
		public static readonly short OP = 226;

		private int _pickTime;

		private int _countdown;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "pickTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pickTime
		{
			get
			{
				return this._pickTime;
			}
			set
			{
				this._pickTime = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "countdown", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int countdown
		{
			get
			{
				return this._countdown;
			}
			set
			{
				this._countdown = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
