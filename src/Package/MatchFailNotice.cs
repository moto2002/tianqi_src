using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(613), ForSend(613), ProtoContract(Name = "MatchFailNotice")]
	[Serializable]
	public class MatchFailNotice : IExtensible
	{
		public static readonly short OP = 613;

		private int _reason;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "reason", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reason
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
