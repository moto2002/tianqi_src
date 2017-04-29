using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "ChapterInfo")]
	[Serializable]
	public class ChapterInfo : IExtensible
	{
		private int _chapterId;

		private readonly List<DungeonInfo> _dungeons = new List<DungeonInfo>();

		private int _totalStar;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "chapterId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, Name = "dungeons", DataFormat = DataFormat.Default)]
		public List<DungeonInfo> dungeons
		{
			get
			{
				return this._dungeons;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "totalStar", DataFormat = DataFormat.TwosComplement)]
		public int totalStar
		{
			get
			{
				return this._totalStar;
			}
			set
			{
				this._totalStar = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
