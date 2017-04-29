using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(85), ForSend(85), ProtoContract(Name = "FlyShoeInfoNty")]
	[Serializable]
	public class FlyShoeInfoNty : IExtensible
	{
		public static readonly short OP = 85;

		private int _todayFreeUseTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "todayFreeUseTimes", DataFormat = DataFormat.TwosComplement)]
		public int todayFreeUseTimes
		{
			get
			{
				return this._todayFreeUseTimes;
			}
			set
			{
				this._todayFreeUseTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
