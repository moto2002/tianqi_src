using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1334), ForSend(1334), ProtoContract(Name = "SurvivalChallengeStartFightReq")]
	[Serializable]
	public class SurvivalChallengeStartFightReq : IExtensible
	{
		public static readonly short OP = 1334;

		private int _difficulty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "difficulty", DataFormat = DataFormat.TwosComplement)]
		public int difficulty
		{
			get
			{
				return this._difficulty;
			}
			set
			{
				this._difficulty = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
