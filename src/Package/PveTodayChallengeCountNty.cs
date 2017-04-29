using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1097), ForSend(1097), ProtoContract(Name = "PveTodayChallengeCountNty")]
	[Serializable]
	public class PveTodayChallengeCountNty : IExtensible
	{
		public static readonly short OP = 1097;

		private int _count;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "count", DataFormat = DataFormat.TwosComplement)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
