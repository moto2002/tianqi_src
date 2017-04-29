using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1060), ForSend(1060), ProtoContract(Name = "TeamMemFightReqConfirmNty")]
	[Serializable]
	public class TeamMemFightReqConfirmNty : IExtensible
	{
		public static readonly short OP = 1060;

		private DungeonType.ENUM _dungeonType;

		private bool _allAgree;

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

		[ProtoMember(2, IsRequired = true, Name = "allAgree", DataFormat = DataFormat.Default)]
		public bool allAgree
		{
			get
			{
				return this._allAgree;
			}
			set
			{
				this._allAgree = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
