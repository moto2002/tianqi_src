using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(883), ForSend(883), ProtoContract(Name = "SecretAreaRankListRes")]
	[Serializable]
	public class SecretAreaRankListRes : IExtensible
	{
		public static readonly short OP = 883;

		private readonly List<SecretAreaRankInfo> _rankInfos = new List<SecretAreaRankInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "rankInfos", DataFormat = DataFormat.Default)]
		public List<SecretAreaRankInfo> rankInfos
		{
			get
			{
				return this._rankInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
