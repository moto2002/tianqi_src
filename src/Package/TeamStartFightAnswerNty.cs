using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4076), ForSend(4076), ProtoContract(Name = "TeamStartFightAnswerNty")]
	[Serializable]
	public class TeamStartFightAnswerNty : IExtensible
	{
		public static readonly short OP = 4076;

		private DungeonType.ENUM _dungeonType;

		private int _dungeonId;

		private int _cd;

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

		[ProtoMember(2, IsRequired = false, Name = "dungeonId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "cd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cd
		{
			get
			{
				return this._cd;
			}
			set
			{
				this._cd = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
