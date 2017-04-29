using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "TeamDetailReason")]
	[Serializable]
	public class TeamDetailReason : IExtensible
	{
		[ProtoContract(Name = "RSType")]
		public enum RSType
		{
			[ProtoEnum(Name = "LvLimit", Value = 1)]
			LvLimit = 1,
			[ProtoEnum(Name = "TimesLimit", Value = 2)]
			TimesLimit,
			[ProtoEnum(Name = "BagFull", Value = 3)]
			BagFull,
			[ProtoEnum(Name = "NotFound", Value = 4)]
			NotFound,
			[ProtoEnum(Name = "InFighting", Value = 5)]
			InFighting,
			[ProtoEnum(Name = "NotAgree", Value = 11)]
			NotAgree = 11,
			[ProtoEnum(Name = "NotAnswer", Value = 12)]
			NotAnswer,
			[ProtoEnum(Name = "DungeonLimit", Value = 13)]
			DungeonLimit,
			[ProtoEnum(Name = "TaskNotFinish", Value = 14)]
			TaskNotFinish,
			[ProtoEnum(Name = "Others", Value = 10000)]
			Others = 10000
		}

		private TeamDetailReason.RSType _reasonType;

		private long _roleId;

		private DungeonType.ENUM _dungeonType;

		private int _dungeonId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "reasonType", DataFormat = DataFormat.TwosComplement)]
		public TeamDetailReason.RSType reasonType
		{
			get
			{
				return this._reasonType;
			}
			set
			{
				this._reasonType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "roleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "dungeonType", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "dungeonId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
