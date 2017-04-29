using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1239), ForSend(1239), ProtoContract(Name = "ChapterAwardChangeNty")]
	[Serializable]
	public class ChapterAwardChangeNty : IExtensible
	{
		public static readonly short OP = 1239;

		private readonly List<ChapterAwardInfo> _chapterAwards = new List<ChapterAwardInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "chapterAwards", DataFormat = DataFormat.Default)]
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
