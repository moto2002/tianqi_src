using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(625), ForSend(625), ProtoContract(Name = "ChallengeWildBossRes")]
	[Serializable]
	public class ChallengeWildBossRes : IExtensible
	{
		public static readonly short OP = 625;

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
