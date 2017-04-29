using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "TypeInfo")]
	[Serializable]
	public class TypeInfo : IExtensible
	{
		private DungeonType.ENUM _dungeonType;

		private readonly List<ChapterInfo> _chapters = new List<ChapterInfo>();

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

		[ProtoMember(2, Name = "chapters", DataFormat = DataFormat.Default)]
		public List<ChapterInfo> chapters
		{
			get
			{
				return this._chapters;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
