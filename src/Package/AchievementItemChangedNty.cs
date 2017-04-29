using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1081), ForSend(1081), ProtoContract(Name = "AchievementItemChangedNty")]
	[Serializable]
	public class AchievementItemChangedNty : IExtensible
	{
		public static readonly short OP = 1081;

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
