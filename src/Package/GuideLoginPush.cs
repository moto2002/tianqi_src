using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(604), ForSend(604), ProtoContract(Name = "GuideLoginPush")]
	[Serializable]
	public class GuideLoginPush : IExtensible
	{
		public static readonly short OP = 604;

		private readonly List<GuideInfo> _guideInfos = new List<GuideInfo>();

		private readonly List<GuideInfo> _notCompleteGuideGroupInfos = new List<GuideInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "guideInfos", DataFormat = DataFormat.Default)]
		public List<GuideInfo> guideInfos
		{
			get
			{
				return this._guideInfos;
			}
		}

		[ProtoMember(2, Name = "notCompleteGuideGroupInfos", DataFormat = DataFormat.Default)]
		public List<GuideInfo> notCompleteGuideGroupInfos
		{
			get
			{
				return this._notCompleteGuideGroupInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
