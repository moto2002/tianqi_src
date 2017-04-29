using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(710), ForSend(710), ProtoContract(Name = "GangFightMatchFail")]
	[Serializable]
	public class GangFightMatchFail : IExtensible
	{
		public static readonly short OP = 710;

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
