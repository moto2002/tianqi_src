using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(229), ForSend(229), ProtoContract(Name = "GuildWarMultiRewardCountdownNty")]
	[Serializable]
	public class GuildWarMultiRewardCountdownNty : IExtensible
	{
		public static readonly short OP = 229;

		private int _countdownSec;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "countdownSec", DataFormat = DataFormat.TwosComplement)]
		public int countdownSec
		{
			get
			{
				return this._countdownSec;
			}
			set
			{
				this._countdownSec = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
