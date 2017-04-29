using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(809), ForSend(809), ProtoContract(Name = "ChallengeDungeonReq")]
	[Serializable]
	public class ChallengeDungeonReq : IExtensible
	{
		public static readonly short OP = 809;

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
