using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6219), ForSend(6219), ProtoContract(Name = "HitMouseSettleReq")]
	[Serializable]
	public class HitMouseSettleReq : IExtensible
	{
		public static readonly short OP = 6219;

		private int _score;

		private int _useSec;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "score", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int score
		{
			get
			{
				return this._score;
			}
			set
			{
				this._score = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "useSec", DataFormat = DataFormat.TwosComplement)]
		public int useSec
		{
			get
			{
				return this._useSec;
			}
			set
			{
				this._useSec = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
