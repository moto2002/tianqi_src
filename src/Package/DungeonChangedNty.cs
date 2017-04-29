using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(729), ForSend(729), ProtoContract(Name = "DungeonChangedNty")]
	[Serializable]
	public class DungeonChangedNty : IExtensible
	{
		public static readonly short OP = 729;

		private DungeonType.ENUM _dungeonType;

		private int _chapterId;

		private int _chapterTotalStar;

		private DungeonInfo _dungeon;

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

		[ProtoMember(2, IsRequired = true, Name = "chapterId", DataFormat = DataFormat.TwosComplement)]
		public int chapterId
		{
			get
			{
				return this._chapterId;
			}
			set
			{
				this._chapterId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "chapterTotalStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapterTotalStar
		{
			get
			{
				return this._chapterTotalStar;
			}
			set
			{
				this._chapterTotalStar = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "dungeon", DataFormat = DataFormat.Default)]
		public DungeonInfo dungeon
		{
			get
			{
				return this._dungeon;
			}
			set
			{
				this._dungeon = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
