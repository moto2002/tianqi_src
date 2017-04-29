using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ChapterResume")]
	[Serializable]
	public class ChapterResume : IExtensible
	{
		private DungeonType.ENUM _dungeonType;

		private int _chapterId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
