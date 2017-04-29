using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(45), ForSend(45), ProtoContract(Name = "ExperienceCopyInfoNty")]
	[Serializable]
	public class ExperienceCopyInfoNty : IExtensible
	{
		public static readonly short OP = 45;

		private int _restChallengeTimes;

		private int _extendTimes;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "restChallengeTimes", DataFormat = DataFormat.TwosComplement)]
		public int restChallengeTimes
		{
			get
			{
				return this._restChallengeTimes;
			}
			set
			{
				this._restChallengeTimes = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "extendTimes", DataFormat = DataFormat.TwosComplement)]
		public int extendTimes
		{
			get
			{
				return this._extendTimes;
			}
			set
			{
				this._extendTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
