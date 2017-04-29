using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(644), ForSend(644), ProtoContract(Name = "DungeonDataPush")]
	[Serializable]
	public class DungeonDataPush : IExtensible
	{
		public static readonly short OP = 644;

		private int _usedFreeMopUpTimes;

		private readonly List<ChapterResume> _chapterInfos = new List<ChapterResume>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "usedFreeMopUpTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int usedFreeMopUpTimes
		{
			get
			{
				return this._usedFreeMopUpTimes;
			}
			set
			{
				this._usedFreeMopUpTimes = value;
			}
		}

		[ProtoMember(2, Name = "chapterInfos", DataFormat = DataFormat.Default)]
		public List<ChapterResume> chapterInfos
		{
			get
			{
				return this._chapterInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
