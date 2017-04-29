using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1080), ForSend(1080), ProtoContract(Name = "AchievementLoginPush")]
	[Serializable]
	public class AchievementLoginPush : IExtensible
	{
		public static readonly short OP = 1080;

		private readonly List<AchievementItemInfo> _achievementItemInfos = new List<AchievementItemInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "achievementItemInfos", DataFormat = DataFormat.Default)]
		public List<AchievementItemInfo> achievementItemInfos
		{
			get
			{
				return this._achievementItemInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
