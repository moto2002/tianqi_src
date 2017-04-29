using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(908), ForSend(908), ProtoContract(Name = "ChapterAwardLoginPush")]
	[Serializable]
	public class ChapterAwardLoginPush : IExtensible
	{
		public static readonly short OP = 908;

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
