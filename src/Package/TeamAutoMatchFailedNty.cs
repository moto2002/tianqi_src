using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4031), ForSend(4031), ProtoContract(Name = "TeamAutoMatchFailedNty")]
	[Serializable]
	public class TeamAutoMatchFailedNty : IExtensible
	{
		public static readonly short OP = 4031;

		private DungeonType.ENUM _dungeonType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dungeonType", DataFormat = DataFormat.TwosComplement)]
		public DungeonType.ENUM dungeonType
		{
			get
			{
				return this._dungeonType;
			}
			set
			{
				this._dungeonType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
