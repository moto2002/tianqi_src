using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(5812), ForSend(5812), ProtoContract(Name = "GetRanInfoRes")]
	[Serializable]
	public class GetRanInfoRes : IExtensible
	{
		public static readonly short OP = 5812;

		private readonly List<OneLvRankInfo> _lvInfo = new List<OneLvRankInfo>();

		private readonly List<OneFightingRankInfo> _fightingInfo = new List<OneFightingRankInfo>();

		private readonly List<OnePetFRankInfo> _petFInfo = new List<OnePetFRankInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "lvInfo", DataFormat = DataFormat.Default)]
		public List<OneLvRankInfo> lvInfo
		{
			get
			{
				return this._lvInfo;
			}
		}

		[ProtoMember(2, Name = "fightingInfo", DataFormat = DataFormat.Default)]
		public List<OneFightingRankInfo> fightingInfo
		{
			get
			{
				return this._fightingInfo;
			}
		}

		[ProtoMember(3, Name = "petFInfo", DataFormat = DataFormat.Default)]
		public List<OnePetFRankInfo> petFInfo
		{
			get
			{
				return this._petFInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
