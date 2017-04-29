using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "PveChapterInfo")]
	[Serializable]
	public class PveChapterInfo : IExtensible
	{
		private int _chapterId;

		private readonly List<PveSectionInfo> _sections = new List<PveSectionInfo>();

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

		[ProtoMember(2, Name = "sections", DataFormat = DataFormat.Default)]
		public List<PveSectionInfo> sections
		{
			get
			{
				return this._sections;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "totalStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
