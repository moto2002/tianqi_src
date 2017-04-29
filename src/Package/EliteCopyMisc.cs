using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "EliteCopyMisc")]
	[Serializable]
	public class EliteCopyMisc : IExtensible
	{
		private int _usedChallengeTimes;

		private int _usedResetChallengeTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "usedChallengeTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int usedChallengeTimes
		{
			get
			{
				return this._usedChallengeTimes;
			}
			set
			{
				this._usedChallengeTimes = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "usedResetChallengeTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int usedResetChallengeTimes
		{
			get
			{
				return this._usedResetChallengeTimes;
			}
			set
			{
				this._usedResetChallengeTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
