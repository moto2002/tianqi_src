using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1413), ForSend(1413), ProtoContract(Name = "SurvivalChallengeBtlProgressNty")]
	[Serializable]
	public class SurvivalChallengeBtlProgressNty : IExtensible
	{
		public static readonly short OP = 1413;

		private float _rate;

		private int _remainCount;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "rate", DataFormat = DataFormat.FixedSize)]
		public float rate
		{
			get
			{
				return this._rate;
			}
			set
			{
				this._rate = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "remainCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainCount
		{
			get
			{
				return this._remainCount;
			}
			set
			{
				this._remainCount = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
