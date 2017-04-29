using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2342), ForSend(2342), ProtoContract(Name = "GetChapterAwardDataRes")]
	[Serializable]
	public class GetChapterAwardDataRes : IExtensible
	{
		public static readonly short OP = 2342;

		private int _canReadChapterAwardId;

		private readonly List<ChapterAwardInfo> _chapterAwards = new List<ChapterAwardInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "canReadChapterAwardId", DataFormat = DataFormat.TwosComplement)]
		public int canReadChapterAwardId
		{
			get
			{
				return this._canReadChapterAwardId;
			}
			set
			{
				this._canReadChapterAwardId = value;
			}
		}

		[ProtoMember(2, Name = "chapterAwards", DataFormat = DataFormat.Default)]
		public List<ChapterAwardInfo> chapterAwards
		{
			get
			{
				return this._chapterAwards;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
