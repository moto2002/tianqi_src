using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(5118), ForSend(5118), ProtoContract(Name = "GetHitMouseRankRes")]
	[Serializable]
	public class GetHitMouseRankRes : IExtensible
	{
		public static readonly short OP = 5118;

		private readonly List<HitMouseRankInfo> _infos = new List<HitMouseRankInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infos", DataFormat = DataFormat.Default)]
		public List<HitMouseRankInfo> infos
		{
			get
			{
				return this._infos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
