using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(946), ForSend(946), ProtoContract(Name = "ResetChallengeTimesReq")]
	[Serializable]
	public class ResetChallengeTimesReq : IExtensible
	{
		public static readonly short OP = 946;

		private int _dungeonId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dungeonId", DataFormat = DataFormat.TwosComplement)]
		public int dungeonId
		{
			get
			{
				return this._dungeonId;
			}
			set
			{
				this._dungeonId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
