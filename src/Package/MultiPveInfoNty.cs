using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(192), ForSend(192), ProtoContract(Name = "MultiPveInfoNty")]
	[Serializable]
	public class MultiPveInfoNty : IExtensible
	{
		public static readonly short OP = 192;

		private int _todayPlayerTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "todayPlayerTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int todayPlayerTimes
		{
			get
			{
				return this._todayPlayerTimes;
			}
			set
			{
				this._todayPlayerTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
