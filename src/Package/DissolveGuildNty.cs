using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(728), ForSend(728), ProtoContract(Name = "DissolveGuildNty")]
	[Serializable]
	public class DissolveGuildNty : IExtensible
	{
		public static readonly short OP = 728;

		private int _contribution;

		private LeaveGuildReason.LGDR _reason;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "contribution", DataFormat = DataFormat.TwosComplement)]
		public int contribution
		{
			get
			{
				return this._contribution;
			}
			set
			{
				this._contribution = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "reason", DataFormat = DataFormat.TwosComplement)]
		public LeaveGuildReason.LGDR reason
		{
			get
			{
				return this._reason;
			}
			set
			{
				this._reason = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
